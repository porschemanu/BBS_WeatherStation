function content_builder(entries_count, array_of_entries, entries_title) {
    i = 0;
    var output = '';

    while (i < entries_count - 1) {
        output += '<div class="content_container">'
            + '<h3>' + entries_title[i] + '</h3>'
            + '<canvas class="diagram"></canvas> '
            + '<script>fDiagram()</script>'
            + '</div>'
        i++;
    }

    return output;
}

function fDiagram_Choice(Entry_Title) {
    var output = ''

    switch (Entry_Title) {
        case 'Luftfeuchtigkeit':
            return "doughnut"

        case 'Temperatur':
            return "bar"

        case 'Luftdruck':
            return "barometer"

        default:
            return "default"
    }
}

function fDiagram(Diagram_Choice, Diagram_Title,xvalues,yvalues) {

    var xValues = [Diagram_Title];
    var yRestValue = 100 - value;
    var yValues = [yRestValue, value];
    var barColors = [
        "#111111",
        "#0000ff"
    ];

    new Chart("myChart", {
        type: 'doughnut',
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

function fTest(){
    let first = 10;
    let second = 20;
    const options = {
        method: 'GET',
        mode: 'no-cors'
    };

        fetch('http://192.168.1.177:5000/add?a='+first+'&b='+second, options)
        .then((response) => {
        return response.json()
        })
        .then((myJson) => {
        console.log("When I add "+first+" and "+second+" I get: " + myJson.result);
        });
        }

//TODO: Settings. json for setting the diagrams

//TODO: Color change for temperatur