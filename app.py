#AUTHOR: Manuel Yates
#LAST UPDATED: 19.12.2021

#NAMECONVENTIONS
#   -Values | Einzelne Werte
#       temperature         =   Temperatur              "Field1"
#       humidity            =   Luftfeuchtigkeit        "Field2"
#       airpressure         =   Luftdruck               "Field3"       
#       last_update         =   Zuletzt aktualisiert    
#   -Lists  | Listen
#       list_temperature    =   Liste aller Temperatur-Daten
#       list_humidity       =   Liste aller Luftfeutigkeit-Daten
#       list_airpressure    =   Liste aller Luftdruck-Daten
#       list_timecode       =   Liste aller Zeitstempel der Daten
#       list_fieldname      =   Liste aller eingelesenen Werte
#       

from flask import Flask, redirect, url_for, render_template
import json
import datetime

app = Flask(__name__)

list_temperature    = []  
list_humidity       = []  
list_airpressure    = []  
list_timecode       = []  
list_fieldname      = []  

def valueloader():
    now = datetime.datetime.now()
    print("Fetching Data from JSON")
    data = json.load(open('data/test.json'))

    print("Reading the number of data fields from the 'channel'")
    list_channel = data['channel']

    for data_entry in list_channel:
        if "field" in data_entry:
            list_fieldname.append(list_channel[data_entry])

    #Sorting the input data fields to seperated lists
    list_feed = data['feeds']

    for data_entry in list_feed:
        list_temperature.append(data_entry['field1'])
        list_humidity.append(data_entry['field2'])
        list_airpressure.append(data_entry['field3'])
        list_timecode.append(data_entry['created_at'])

@app.route("/")
def home():
    valueloader()
    return render_template("index.html", 
                            temperature = list_temperature[-1],
                            humidity = list_humidity[-1],
                            airpressure = list_airpressure[-1],
                            last_update = list_timecode[-1],
                            list_temperature = list_temperature,
                            list_humidity = list_humidity,
                            list_airpressure = list_airpressure,
                            list_timecode = list_timecode,
                            list_fieldname = list_fieldname)


if __name__ == "__main__":
    app.run()

