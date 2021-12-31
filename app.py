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
    SettingsDataRAW = json.load(open('settings/settings.json'))
    return SettingsDataRAW;

#Renderer render_template("index.html"
@app.route("/")
def index():
    print(GetWeatherData())
    return render_template("index.html")

@app.route("/widget")
def widget():
    return render_template("widget.html")

@app.route("/settings")
def settings():
    return render_template("settings.html", SettingsDataRAW = GetSettings() )

@app.route("/save_settings/<setting>")
def SaveSettings():
    #TODO Save Mechanism
    return "true"

if __name__ == "__main__":
    app.run()
