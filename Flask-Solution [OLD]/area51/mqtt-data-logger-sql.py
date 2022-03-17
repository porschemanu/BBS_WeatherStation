#!python3
###demo code provided by Steve Cope at www.steves-internet-guide.com
##email steve@steves-internet-guide.com
###Free to use for any purpose

"""
This will log messages to file.Los time,message and topic as JSON data
"""
import paho.mqtt.client as mqtt
import json
import os
import time
import sys, getopt,random
import logging
#import mlogger as mlogger
from sql_logger import SQL_data_logger
import threading
from queue import Queue
q=Queue()

##### User configurable data section
username=""
password=""
verbose=False #True to display all messages, False to display only changed messages
mqttclient_log=False #MQTT client logs showing messages
logging.basicConfig(level=logging.INFO) #error logging
#use DEBUG,INFO,WARNING
####
options=dict()
##EDIT HERE ###############
brokers=["192.168.1.51","192.168.1.186","192.168.1.206","192.168.1.185",\
         "test.mosquitto.org","broker.hivemq.com","iot.eclipse.org"]
options["broker"]=brokers[0]
options["port"]=1883
options["verbose"]=True
options["cname"]=""
options["username"]=""
options["password"]=""
options["topics"]=[("$SYS/#",0)]

#sql
db_file="logs.db"
Table_name="logs"
table_fields={
    "id":"integer primary key autoincrement",
    "time":"int",
    "topic":"text",
    "sensor":"text",
    "message": "text",}
###
cname=""
sub_flag=""
timeout=60
messages=dict()
last_message=dict()
######
def command_input(options={}):
    topics_in=[]
    qos_in=[]

    valid_options=" -b <broker> -p <port>-t <topic> -q QOS -v <verbose> -h <help>\
-c <loop Time secs -d logging debug  -n Client ID or Name\
-i loop Interval -u Username -P Password\
"
    print_options_flag=False
    try:
      opts, args = getopt.getopt(sys.argv[1:],"hb:i:dk:p:t:q:l:vn:u:P:")
    except getopt.GetoptError:
      print (sys.argv[0],valid_options)
      sys.exit(2)
    qos=0

    for opt, arg in opts:
        if opt == '-h':
            print (sys.argv[0],valid_options)
            sys.exit()
        elif opt == "-b":
             options["broker"] = str(arg)
        elif opt == "-i":
             options["interval"] = int(arg)
        elif opt == "-k":
             options["keepalive"] = int(arg)
        elif opt =="-p":
            options["port"] = int(arg)
        elif opt =="-t":
            topics_in.append(arg)
        elif opt =="-q":
             qos_in.append(int(arg))
        elif opt =="-n":
             options["cname"]=arg
        elif opt =="-d":
            options["loglevel"]="DEBUG"
        elif opt == "-P":
             options["password"] = str(arg)
        elif opt == "-u":
             options["username"] = str(arg)        
        elif opt =="-v":
            options["verbose"]=True
      

    lqos=len(qos_in)
    for i in range(len(topics_in)):
        if lqos >i: 
            topics_in[i]=(topics_in[i],int(qos_in[i]))
        else:
            topics_in[i]=(topics_in[i],0)         
        
    if topics_in:
        options["topics"]=topics_in #array with qos

####

#callbacks -all others define in functions module
def on_connect(client, userdata, flags, rc):
    logging.debug("Connected flags"+str(flags)+"result code "\
    +str(rc)+"client1_id")
    if rc==0:
        client.connected_flag=True
    else:
        client.bad_connection_flag=True

def on_disconnect(client, userdata, rc):
    logging.debug("disconnecting reason  " + str(rc))
    client.connected_flag=False
    client.disconnect_flag=True
    client.subscribe_flag=False
    
def on_subscribe(client,userdata,mid,granted_qos):
    m="in on subscribe callback result "+str(mid)
    logging.debug(m)
    client.subscribed_flag=True
def on_message(client,userdata, msg):
    topic=msg.topic
    m_decode=str(msg.payload.decode("utf-8","ignore"))
    message_handler(client,m_decode,topic)
    #print("message received")
def message_handler(client,msg,topic):
    data=dict()
    tnow=time.localtime(time.time())
    m=time.asctime(tnow)+" "+topic+" "+msg
    data["time"]=int(time.time())
    data["topic"]=topic
    data["message"]=msg
    if has_changed(topic,msg):
        #print("storing changed data",topic, "   ",msg)
        q.put(data) #put messages on queue

def has_changed(topic,msg):
    topic2=topic.lower()
    if topic in last_message:
        if last_message[topic]==msg:
            return False
    last_message[topic]=msg
    return True
def log_worker():
    """runs in own thread to log data"""
    #create logger
    logger=SQL_data_logger(db_file)
    logger.drop_table("logs")
    logger.create_table("logs",table_fields)
    while Log_worker_flag:
        time.sleep(0.01)
        while not q.empty():
            data = q.get()
            if data is None:
                continue
            try:
                timestamp=data["time"]
                topic=data["topic"]
                message=data["message"]
                sensor="Dummy-sensor"
                data_out=[timestamp,topic,sensor,message]
                data_query="INSERT INTO "+ \
                Table_name +"(time,topic,sensor,message)VALUES(?,?,?,?)"   
                logger.Log_sensor(data_query,data_out)
            except Exception as e:
                print("problem with logging ",e)
    logger.conn.close()

            #print("message saved ",results["message"])


########################
####

    
def Initialise_clients(cname,cleansession=True):
    #flags set
    client= mqtt.Client(cname)
    if mqttclient_log: #enable mqqt client logging
        client.on_log=on_log
    client.on_connect= on_connect        #attach function to callback
    client.on_message=on_message        #attach function to callback
    client.on_disconnect=on_disconnect
    client.on_subscribe=on_subscribe
    return client
###



###########
def convert(t):
    d=""
    for c in t:  # replace all chars outside BMP with a !
            d =d+(c if ord(c) < 0x10000 else '!')
    return(d)
def print_out(m):
    if display:
        print(m)

    
########################main program
if __name__ == "__main__" and len(sys.argv)>=2:
    command_input(options)
    pass
verbose=options["verbose"]

if not options["cname"]:
    r=random.randrange(1,10000)
    cname="logger-"+str(r)
else:
    cname="logger-"+str(options["cname"])




        
#Initialise_client_object() # add extra flags
logging.info("creating client "+cname)
client=Initialise_clients(cname,False)#create and initialise client object
topics=options["topics"]
broker=options["broker"]
port=1883
keepalive=60
if options["username"] !="":
    print("setting username")
    client.username_pw_set(options["username"], options["password"])
print("starting")

##
t = threading.Thread(target=log_worker) #start logger
Log_worker_flag=True
t.start() #start logging thread
###
client.connected_flag=False # flag for connection
client.bad_connection_flag=False
client.subscribed_flag=False
client.loop_start()
client.connect(broker,port)
while not client.connected_flag: #wait for connection
    time.sleep(1)
client.subscribe(topics)
while not client.subscribed_flag: #wait for connection
    time.sleep(1)
    print("waiting for subscribe")
print("subscribed ",topics)
#loop and wait until interrupted
try:
    while True:
        pass
except KeyboardInterrupt:
    print("interrrupted by keyboard")


client.loop_stop()  #final check for messages
time.sleep(5)
Log_worker_flag=False #stop logging thread
print("ending ")

