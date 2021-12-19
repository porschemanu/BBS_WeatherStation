function fPie(humidity) {
    var yValue = humidity;
    var xValues = ["Luftfeuchtigkeit"];
    var yRestValue = 100 - yValue;
    var yValues = [yRestValue, yValue];
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





