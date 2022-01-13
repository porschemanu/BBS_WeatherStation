let WeatherDataArray;
let ListOfValueNames;
let CountOfValueEntries;
let CountOfValueNames;
let SettingsData;

//GLOBAL FUNCTIONS
function set_global(iWeatherDataArray,iListOfValueNames,iCountOfValueEntries,iCountOfValueNames,iSettingsDataRAW){
    WeatherDataArray = iWeatherDataArray;
    ListOfValueNames = iListOfValueNames;
    CountOfValueEntries = iCountOfValueEntries;
    CountOfValueNames = iCountOfValueNames;
    SettingsData = load_settings(iSettingsDataRAW);
    console.log('Values Import Successful')
}

function load_settings(SettingsDataRaw){
    SettingsDataRAW_parsed = JSON.parse(SettingsDataRaw);
    output = []

    for(var i in SettingsDataRAW_parsed)

        output.push(SettingsDataRAW_parsed[i][0])
    return output;
}

function save_settings(){
    var SettingsArray = fetch_settings()
}

//INDEX FUNCTIONS
function index_build_mask() {
/*
1.Step
Build the mask with the needed canvases.
 */
    Mask = ''
    switch (CountOfValueNames) {
        case 0:
            Mask += '<h1>Es konnten keine Werte ausgelesen werden!</h1>'
            break;
        case 3:
            Mask += '<table class = "index_diagram_three">'
            Mask += '<tr>' + index_build_cell() + '</tr>'
            Mask += '</table>'
            break;
        default:
            Mask += '<h1>Fehler!</h1>'
            break;
    }
    return Mask;
}

function index_build_cell(){
/* Just put the ting from above into this
* Create Logic for more than 4 Values
*
* */
    var Cells = ''
    i = 0
    while( i < CountOfValueNames ){
        Cells += '<td><h1>' + ListOfValueNames[i] + '</h1><div id="'+ ListOfValueNames[i] +'"><canvas id="diagram_'+ ListOfValueNames[i] +'" style="width:100%;max-width:700px"></canvas></div>'
        i++;
    }
    console.log('Cell Building Successful')
    return Cells
}

function index_fill_mask(){
/*
2.Step:
Filling the prebuild canvas with our diagrams

if(SettingsData[i].DataName == ValueName){
            stringtemp = SettingsEntries[i].DiagramType
        }
*/

    for (entry in ListOfValueNames){
        let ValueSettings = ''
        for (i in SettingsData){
            if(SettingsData[i].DataName == ListOfValueNames[entry]){
                ValueSettings = SettingsData[i].DiagramType
            }
        }

        if(ValueSettings == ''){
            return "FUCK"
        }

        switch (ValueSettings){
            case 'text':
                DiagramText(WeatherDataArray[0][+entry + 1],ListOfValueNames[entry])
                break;

            case 'pie':
                DiagramPie(WeatherDataArray[0][+entry + 1],ListOfValueNames[entry]) //+1 because of the UTC Timestamp at Array-Place 0
                break;

            default:
                break;
        }
    }
}



//SETTINGS FUNCTIONS

function settings_build_mask(){
    let Mask = '<div id="settings_table"><table>'
        Mask += '<th>DataName</th><th>DiagramType</th><th>DataFormat</th>'
        Mask += settings_build_row();
        Mask += '</table><br><button onClick="save_settings()">Submit</button</div>'
    return Mask
}

function settings_build_row(){
    Row = '';
    i = 0
    for(i in SettingsData){
        Row += '<tr>'
        Row += '<td>' + SettingsData[i].DataName + '</td><td>' + settings_options_diagramchoice(SettingsData[i].DataName) + '</td><td><input id="input_'+ SettingsData[i].DataName +'"></td>'
        Row += '</tr>'
    }
    return Row;
}

function settings_options_diagramchoice(DataName){
    options = ['text','pie','bar']

    choice = '<select id="select_' + DataName + '">'
        i = 0
        for (i in options){
            choice += '<option value="' + options[i] + '">' + options[i] + '</option>'
        }
    choice += '</select>'

    return choice
}

function fetch_settings(){
    SettingsArray = [];
    webpath = ''
    i = 0
    for(i in SettingsData){
        webpath += SettingsData[i].DataName+ ':' +document.getElementById('select_' + SettingsData[i].DataName).value + ';'
        SettingsArray.push()
    }
    var stream =

    fetch('http://127.0.0.1:5000/save_settings/' + webpath)
    //.then(response => response.text())
    //if (response.text() == "true"){alert("HalloWelt")}
}

//DIAGRAM FUNCTIONS
function DiagramPie(Value, Target){
    var yValues = [Value,100 -Value];
    var barColors = ["#b91d47"];


    new Chart('diagram_'+Target, {
        type: "pie",
        data: {
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            plugins: {
                legend: false,
                tooltip: false,
            }
        }
    });
}

function DiagramBar(Value, Target){

}

function DiagramText(Value, Target){
    document.getElementById(Target).innerHTML = '<h1>'+Value +'</h1>'
}
