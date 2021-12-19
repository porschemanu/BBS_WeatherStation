# field 1: temperatur, field 2: humidity, field 3: Luftdruck

from flask import Flask, redirect, url_for, render_template
import json
import datetime



app = Flask(__name__)

now = datetime.datetime.now()

temperatur_list = []
humidity_list = []
pressure_list = []

def valueloader():
    data = json.load(open('data/test.json'))
    #print("Fetching Data from JSON")
    listvalue = data['feeds']
    
    for x in listvalue:
        temperatur_list.append(x['field1'])
        humidity_list.append(x['field2'])
        pressure_list.append(x['field3'])

@app.route("/")
def home():
    while now.hour < 24:
        valueloader()
        return render_template("index.html", humidity= humidity_list[-1], airpressure = pressure_list[-1], temperature = temperatur_list[-1])
        sleep(5)


if __name__ == "__main__":
    app.run()

