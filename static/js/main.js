function index_builder() {
  
}

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

    output = '<div id="settings_table"><table>'
    i = 0

    while(i < CountOfEntries){
        _TEMP = Object.values(inputarray[i])
        output += '<tr><td>'+ _TEMP[0] +':</td><td><select></td></tr>'
        i++
    }

    output += '</table><br><button onClick="SaveSettings()">Test</button</div>' 
    return output
}

function FetchSettings(){
    console.log(document.getElementById('settings_table').textContent)
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