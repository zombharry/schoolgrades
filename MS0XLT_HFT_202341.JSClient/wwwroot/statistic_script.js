let Gradeavgs = [];
let Credits = [];
let Merged = [];
let connection = null;

let studentIdToUpdate = -1;
getData();
setupSignalR();
function setupSignalR()
{
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:48224/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("StudentCreated", (user, message) => {
        getData();
    });

    connection.on("StudentDeleted", (user, message) => {
        getData();
    });
    connection.on("StudentUpdated", (user, message) => {
        getData();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start()
{
    try {
        await connection.start();
        console.log("SignalR Connected");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}
async function getData() {
    await fetch('http://localhost:48224/stat/AllAvarageGrade/')
        .then(x => x.json())
        .then(y => {
            Gradeavgs = y;
            
        }).
        then (fetch('http://localhost:48224/stat/StudentsCredits/')
        .then(x => x.json())
        .then(y => {
            Credits = y;
            merger();
            display();
        }));

}
function merger()
{

    //for (var i = 0; i < length; i++) {
    //    Gradeavgs[i].numberOfCredits = Credits[i].numberOfCredits;
    //}
    let creditItem;
    Gradeavgs.forEach(function (x) {
        creditItem = Credits.find(function (y) {
            return y.studentId == x.studentId;
        });
        if (creditItem) {
            Merged.push({
                studentId: x.studentId,
                gradeAvg: x.gradeAvg,
                numberOfCredits: creditItem.numberOfCredits
            })
        }
    });
}


function display()
{
    document.getElementById('resultArea').innerHTML = "";
    Merged.forEach(t => {
        document.getElementById('resultArea').innerHTML +=
            `<tr>
                <td>${t.studentId}</td>
                <td>${t.numberOfCredits} </td>
                <td>${t.gradeAvg} </td>
                
            </tr>`;
    })
}


