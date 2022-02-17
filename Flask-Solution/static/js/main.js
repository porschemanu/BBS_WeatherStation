let WeatherDataArray = new  Array();
let ListOfValueNames = new Array();
let SettingsData;


//GLOBAL FUNCTIONS
function set_global(iSettingsDataRAW, iWeatherData){
    SettingsData = load_settings(iSettingsDataRAW);
    parse_weatherdata(iWeatherData);
    console.log('Values Import Successful')
}



function parse_weatherdata(WeatherData){
    for (i in Object.keys(WeatherData['channel'])){
        if(Object.keys(WeatherData['channel'])[i].includes('field')){
            ListOfValueNames.push(Object.values(WeatherData['channel'])[i]);
        }
    }

    for(i in WeatherData['feeds']){
        WeatherDataArray.push(Object.values(WeatherData['feeds'][i]))
        }

    WeatherDataArray.reverse();

    for (i in WeatherDataArray){
        WeatherDataArray[i].splice(1,1)
    }

    document.getElementById('last_update').innerHTML = "Timestamp des letzten Wertes:" + WeatherDataArray[0][0]

}

function load_settings(SettingsDataRaw){
    SettingsDataRAW_parsed = JSON.parse(SettingsDataRaw);
    output = []

    for(var i in SettingsDataRAW_parsed)
        output.push(SettingsDataRAW_parsed[i][0])
    return output;
}

function save_settings(){
    let webpath = ''
    let i = 0
    /*
    for(i in SettingsData){
        webpath += SettingsData[i].DataName+ ':' +document.getElementById('select_' + SettingsData[i].DataName).value + ';'
    }
    */

    for(i in ListOfValueNames){
        webpath += ListOfValueNames[i]+ ':' +document.getElementById('select_' + ListOfValueNames[i]).value + ';'
    }
    
    fetch('http://127.0.0.1:5000/save_settings/' + webpath)
    //.then(response => response.text())
    //if (response.text() == "true"){alert("HalloWelt")}
}

//INDEX FUNCTIONS
function index_build_mask() {
/*
1.Step
Build the mask with the needed canvases.
 */
    Mask = ''
    switch (ListOfValueNames.length) {
        case 0:
            Mask += '<h1>Es konnten keine Werte ausgelesen werden!</h1>'
            break;
        case 1:
            Mask += '<table class = "index_diagram_one">'
            Mask += '<tr>' + index_build_cell(1) + '</tr>'
            Mask += '</table>'
            break;
        case 2:
            Mask += '<table class = "index_diagram_two">'
            Mask += '<tr>' + index_build_cell(2) + '</tr>'
            Mask += '</table>'
            break;
        case 3:
            Mask += '<table class = "index_diagram_three">'
            Mask += '<tr>' + index_build_cell(3) + '</tr>'
            Mask += '</table>'
            break;
        case 4:
            Mask += '<table class = "index_diagram_four">'
            Mask += '<tr>' + index_build_cell(2) + '</tr>'
            Mask += '</table>'
            break;
        default:
            Mask += '<table class = "index_diagram_Ofour">'
            Mask += index_build_row() //<- Just for testing, Edit later
            Mask += '</table>'
            break;
    }
    return Mask;
}


let dynamiccell = 0;

function index_build_row(){
    let CellsPerRow = 3
    let rowcount = Math.ceil(ListOfValueNames.length / CellsPerRow)
    let Row = ''
    let i = 0

    while (i < rowcount){
        Row += '<tr>'
        Row += index_build_cell(CellsPerRow)
        Row += '</tr>'
        i++
    }
    return Row;
}


function index_build_cell(CellsPerRow){
/* Just put the ting from above into this
* Create Logic for more than 4 Values
*
* */
    var Cells = ''
    i = 0
    while(i < CellsPerRow && i < ListOfValueNames.length){
        Cells += '<td><h1>' + ListOfValueNames[dynamiccell] + '</h1><div id="'+ ListOfValueNames[dynamiccell] +'"><canvas id="diagram_'+ ListOfValueNames[dynamiccell] +'" style="width:100%;max-width:700px"></canvas></div>'
        i ++
        dynamiccell ++
    }
    console.log('Cell Building Successful')
    return Cells
}

function index_fill_mask(){
/*
2.Step:
Filling the prebuild canvas with our diagrams
*/

    for (entry in ListOfValueNames){
        let ValueSettings = ''
        for (i in SettingsData){
            if(SettingsData[i].DataName == ListOfValueNames[entry]){
                ValueSettings = SettingsData[i].DiagramType
            }
        }

        if(ValueSettings == ''){
            //alert("FUCK")
        }

        switch (ValueSettings){
            case 'text':
                DiagramText(WeatherDataArray[0][+entry + 1],ListOfValueNames[entry])
                break;

            case 'pie':
                DiagramPie(WeatherDataArray[0][+entry + 1],ListOfValueNames[entry]) //+1 because of the UTC Timestamp at Array-Place 0
                break;

            case 'bar':
                DiagramBar(WeatherDataArray[0][+entry + 1],ListOfValueNames[entry]) //+1 because of the UTC Timestamp at Array-Place 0
                break;

            default:
                break;
        }
    }
}



//SETTINGS FUNCTIONS

function settings_build_mask(){

    /*
        Mask += '<h1>Settings.json</h1><table>'
        Mask += '<th>DataName</th><th>DiagramType</th><th>DataFormat</th>'
        Mask += settings_build_row();
        Mask += '</table><br>'
    * */

    let Mask = '<div id="settings_table">'
        Mask += '<h1>LiveFeed</h1><table>'
        Mask += '<th>DataName</th><th>DiagramType</th><th>DataFormat</th>'
        Mask += settings_build_row_live();
        Mask += '</table><br>'
        Mask += '<button onClick="save_settings()">Submit</button</div>'
    return Mask
}

function settings_build_row(){ //<- Work with stored Data from Settings.json
    Row = '';
    i = 0
    for(i in SettingsData){
        Row += '<tr>'
        Row += '<td>' + SettingsData[i].DataName + '</td><td>' + settings_options_diagramchoice(SettingsData[i].DataName) + '</td><td><input id="input_'+ SettingsData[i].DataName +'"></td>'
        Row += '</tr>'
    }
    return Row;
}

function settings_build_row_live(){
    Row = '';
    i = 0
    for(i in ListOfValueNames){
        Row += '<tr>'
        Row += '<td>' + ListOfValueNames[i] + '</td><td>' + settings_options_diagramchoice(ListOfValueNames[i]) + '</td><td><input id="input_'+ ListOfValueNames[i] +'"></td>'
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

/* Hallo Welt */

function DiagramText(Value, Target){
    document.getElementById(Target).innerHTML = '<h1>'+Value +'</h1>'
}
