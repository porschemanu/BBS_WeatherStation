function fPie(value) {
    var xValues = ["Luftfeuchtigkeit"];
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
};


function content_builder(entries_count,array_of_entries){
    output = new String;

    while(i < entries_count){
        output += '<div>'
        
        



        output += '</div>'
        i++;
    }

    return output;
}


//Color change for temperatur