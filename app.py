from flask import Flask, redirect, url_for, render_template
import json
app = Flask(__name__)

def GetWeatherData():
    WeatherDataRAW = json.load(open('data/test.json'))
    return WeatherDataRAW;

def GetSettings():
    SettingsDataRAW = json.load(open('settings/settings.json'))
    return SettingsDataRAW;

#Renderer render_template("index.html"
@app.route("/")
def index():
    return render_template("index.html",SettingsDataRAW = GetSettings(),WeatherData = GetWeatherData())

@app.route("/widget")
def widget():
    return render_template("widget.html")

@app.route("/settings")
def settings():
    return render_template("settings.html",SettingsDataRAW = GetSettings(),WeatherData = GetWeatherData())

@app.route("/save_settings/<setting>")
def SaveSettings(setting):
    ParameterList = setting.split(";")
    del ParameterList[-1]

    data = {}
    i = 0
    while i < len(ParameterList):
        tempstring = f'Value{i+1}'
        data[tempstring] = []
        temp = ParameterList[i].split(':')
        data[tempstring].append({"DataName" : temp[0], "DiagramType" : temp[1]})
        i = i + 1

    with open('settings/settings.json', 'w') as outfile:
        json.dump(data, outfile)

    return "true"

if __name__ == "__main__":
    app.run()
