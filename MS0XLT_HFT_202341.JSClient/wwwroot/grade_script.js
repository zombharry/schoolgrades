let grades = [];
let connection = null;

let gradeIdToUpdate = -1;
getData();
setupSignalR();
function setupSignalR()
{
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:48224/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GradeCreated", (user, message) => {
        getData();
    });

    connection.on("GradeDeleted", (user, message) => {
        getData();
    });
    connection.on("GradeUpdated", (user, message) => {
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
    await fetch('http://localhost:48224/grade')
        .then(x => x.json())
        .then(y => {
            grades = y;
            console.log(y);
            display();
        });
}



function display()
{
    document.getElementById('resultArea').innerHTML = "";
    grades.forEach(t => {
        document.getElementById('resultArea').innerHTML +=
            `<tr>
                <td>${t.gradeId}</td>
                <td>${t.studentId}</td>
                <td>${t.subjectId} </td>
                <td>${t.gradeValue} </td>
                 <td>${t.date} </td>
                <td>
                    <button onclick="remove(${t.gradeId})">Delete</button>
                    <button onclick="showUpdate(${t.gradeId})">Edit</button>
                 </td>
            </tr>`;
    })
}
function showUpdate(id)
{

    document.getElementById('studentidtoupdate').value = grades.find(t => t['gradeId'] == id)['studentId'];
    document.getElementById('subjectidtoupdate').value = grades.find(t => t['gradeId'] == id)['subjectId'];
    document.getElementById('gradevaluetoupdate').value = grades.find(t => t['gradeId'] == id)['gradeValue'];
    document.getElementById('datetoupdate').value = dateFormatter(grades.find(t => t['gradeId'] == id)['date']);
    document.getElementById('updateformdiv').style.display = 'flex';
    gradeIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let studentidtoupdate = document.getElementById('studentidtoupdate').value;
    let subjectidtoupdate = document.getElementById('subjectidtoupdate').value;
    let gradevalue = document.getElementById('gradevaluetoupdate').value;
    let date = document.getElementById('datetoupdate').value;
    fetch('http://localhost:48224/grade', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                gradeId: gradeIdToUpdate,
                studentId: studentidtoupdate,
                subjectId: subjectidtoupdate,
                gradeValue: gradevalue,
                date:date
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error', error); });

}

function remove(id)
{
    fetch('http://localhost:48224/grade/'+id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error', error); });
}
function create()
{
    
    const date = new Date();
    

    const formattedDate = dateFormatter(date);

    let studentid = document.getElementById('studentid').value;
    let subjectid = document.getElementById('subjectid').value;
    let gradevalue = document.getElementById('grade_val').value;
    fetch('http://localhost:48224/grade', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                
                studentId: studentid,
                subjectId: subjectid,
                gradeValue: gradevalue,
                date: formattedDate
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error', error); });
    
}
function dateFormatter(date)
{
    let mydate=new Date(date);
    let year = mydate.getFullYear();
    let month = `0${mydate.getMonth() + 1}`.slice(-2);
    let day = `0${mydate.getDate()}`.slice(-2);

    return `${year}-${month}-${day}`;
}