function Diagram(){

}


//BUILDER

function index_builder(WeatherDataArray,ListOfValueNames,CountOfValueEntries,CountOfValueNames,SettingsDataRAW) {
    /*
    Handles the incoming Count of Values:
    1 = Fullscreen of the one Value
    2 = 50|50 
    3 = 33|33|33
    4 = 50|50
        50|50
    >4 = 3 Rows. Filled from left to right
    */



    switch (CountOfValueNames){
        case 0:
            output += '<h1>Es konnten keine Werte ausgelesen werden!</h1>'
            break;
        case 1:
            output = '<table class = "index_diagram_one">'
            output += '<tr><td>'+ get_diagram(ListOfValueNames[0],SettingsDataRAW) +'</td></tr>'
            output += '</table>'
            break;
        case 2:
            output = '<table class = "index_diagram_two">'
            output += '<tr><td>'+ get_diagram(ListOfValueNames[0],SettingsDataRAW) +'</td><td>'+ get_diagram(ListOfValueNames[1],SettingsDataRAW) +'</td></tr>'
            output += '</table>'
            break;
        case 3:
            output = '<table class = "index_diagram_three">'
            output += '<tr><td>'+ get_diagram(ListOfValueNames[0],SettingsDataRAW) +'</td><td>'+ get_diagram(ListOfValueNames[1],SettingsDataRAW) +'</td><td>'+ get_diagram(ListOfValueNames[2],SettingsDataRAW) +'</td></tr>'
            output += '</table>'
            break;
        case 4:
            output = '<table class = "index_diagram_four">'
            output += '<tr><td>'+ get_diagram(ListOfValueNames[0],SettingsDataRAW) +'</td><td>'+ get_diagram(ListOfValueNames[1],SettingsDataRAW) +'</td></tr>'
            output += '<tr><td>'+ get_diagram(ListOfValueNames[2],SettingsDataRAW) +'</td><td>'+ get_diagram(ListOfValueNames[3],SettingsDataRAW) +'</td></tr>'
            output += '</table>'
            break;
        default:
            break;
    }

    return output
}

function get_diagram(ValueName,SettingsDataRaw){

    var SettingsEntries = LoadSettings(SettingsDataRaw)
    var tempArray = []

    for (i in SettingsEntries){
        tempArray.push(SettingsEntries[i].DataName)
    }


    console.log(tempArray.includes(ValueName))
    return ValueName
}

//widget.html

function widget_builder(){

}

//settings.html

function LoadSettings(SettingsDataRaw){
    SettingsDataRAW_parsed = JSON.parse(SettingsDataRaw);
    output = []

    for(var i in SettingsDataRAW_parsed)
        output.push(SettingsDataRAW_parsed[i])
    return output;
}

function settings_builder(inputarray){
    CountOfEntries = inputarray.length
    temp = ''

    output = '<div id="settings_table"><table>'
    i = 0


    while(i < CountOfEntries){
        _TEMP = Object.values(inputarray[i])
        output += '<tr><td><input id="'+ temp.concat("input", i) +'" value="'+ _TEMP[0] +':"></td><td><select id="Test'+ i +'"><option value="Diagram1">Diagram1</option><option value="Diagram2">Diagram2</option><option value="Diagram3">Diagram3</option></td></tr>'
        i++
    }

    output += '</table><br><button onClick="SaveSettings()">Submit</button</div>' 
    return output
}

function FetchSettings(){
    var count = document.getElementsByTagName("tr").length
    console.log(count)
    temp = ''
    i = 0
    while(i < count){
        console.log(document.getElementById(temp.concat('Test', i)).value)
        i++
    }
    return "todo"
}

function SaveSettings(){
    FetchSettings()

    //fetch('http://127.0.0.1:5000/save_settings/HalloWelt')
    //.then(response => response.text())
    //if (response.text() == "true"){}
    alert("Ihr Einstellungen wurden gespeichert")
    
}
    

//TODO: Color change for temperatur
//TODO: Research all Diagram Types for Dropdown Menu in Table