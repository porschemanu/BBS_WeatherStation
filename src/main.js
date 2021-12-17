function fPie(humidity){
    var yValue = humidity;
    var xValues = ["Luftfeuchtigkeit"];
    var yRestValue = 100 - yValue;
    var yValues = [yRestValue,yValue];
    var barColors = [
      "#111111",
      "#ffffff"
    ];
    
    new Chart("myChart", {
        type: 'pie',
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





