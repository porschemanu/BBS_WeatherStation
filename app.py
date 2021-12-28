from flask import Flask, redirect, url_for, render_template
import json
app = Flask(__name__)

#Data Fetching Functions
def GetWeatherData():
    WeatherDataRAW = json.load(open('data/test.json'))

    ListOfChannelData = WeatherDataRAW['channel']
    ListOfValueEntries = WeatherDataRAW['feeds']
    ListOfValueNames = []
    ListOfValueNames.clear()

    #Getting the Name of the Values from the Channel
    for DataEntry in ListOfChannelData:
        if "field" in DataEntry:
            ListOfValueNames.append(ListOfChannelData[DataEntry])

    CountOfValueNames = len(ListOfValueNames) + 1
    CountOfValueEntries = len(ListOfValueEntries)

    #Building the Structure of the Array
    WeatherDataArray = [[0 for x in range(CountOfValueNames)]
                        for x in range(CountOfValueEntries)]

    #Initialising the needed Values
    _temp = ''
    x = 0
    y = 0

    #Filling the Array with Data
    while x < CountOfValueEntries:
        DataEntry = ListOfValueEntries[x]
        y = 0
        while y < CountOfValueNames:
            if y == 0:
                WeatherDataArray[x][0] = DataEntry['created_at']
            elif y < 4:
                _temp = 'field' + str(y)
                WeatherDataArray[x][y] = DataEntry[_temp]
            y += 1
        x += 1

    return WeatherDataArray, ListOfValueNames, CountOfValueEntries, CountOfValueNames - 1

def GetSettings():
    return "todo"

#Renderer render_template("index.html"
@app.route("/")
def index():
    print(GetWeatherData())
    return "todo"

@app.route("/widget")
def widget():
    return "todo"

@app.route("/settings")
def settings():
    return "todo"

@app.route("/save_settings/<setting>")
def SaveSettings():
    return "todo"




def valueloader():
    data = json.load(open('data/test.json'))

    list_fieldname = []

    list_channel = data['channel']
    list_fieldname.clear
    for data_entry in list_channel:
        if "field" in data_entry:
            list_fieldname.append(list_channel[data_entry])

    print(list_fieldname)

    list_feed = data['feeds']
    numberentries = len(list_feed)
    numberdata = len(list_fieldname) + 1
    output_array = [[0 for x in range(numberdata)]
                    for x in range(numberentries)]
    tempstring = ''
    x = 0
    y = 0
    while x < numberentries:
        data_entry = list_feed[x]
        y = 0
        while y < numberdata:
            if y == 0:
                output_array[x][0] = data_entry['created_at']
            elif y < 4:
                tempstring = 'field' + str(y)
                output_array[x][y] = data_entry[tempstring]
            y += 1
        x += 1
    return numberdata, output_array, list_fieldname

def save_settings(setting):
    return setting

if __name__ == "__main__":
    app.run()
