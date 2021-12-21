from flask import Flask, redirect, url_for, render_template
import json
import datetime

app = Flask(__name__)

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
    output_array = [[0 for x in range(numberdata)]for x in range(numberentries)]    
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


@app.route("/")
def home():
    numberdata, output_array, list_fieldname = valueloader();
    return render_template("index.html",entries_number = numberdata ,input_array = output_array, entries_title= list_fieldname)


if __name__ == "__main__":
    app.run()
