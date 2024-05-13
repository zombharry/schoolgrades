let students = [];
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
    await fetch('http://localhost:48224/student')
        .then(x => x.json())
        .then(y => {
            students = y;
            display();
        });
}



function display()
{
    document.getElementById('resultArea').innerHTML = "";
    students.forEach(t => {
        document.getElementById('resultArea').innerHTML +=
            `<tr>
                <td>${t.studentId}</td>
                <td>${t.studentName} </td>
                <td>${t.semester} </td>
                <td>
                    <button onclick="remove(${t.studentId})">Delete</button>
                    <button onclick="showUpdate(${t.studentId})">Edit</button>
                 </td>
            </tr>`;
    })
}
function showUpdate(id)
{

    document.getElementById('studentnametoupdate').value = students.find(t => t['studentId'] == id)['studentName']
    document.getElementById('semestertoupdate').value = students.find(t => t['studentId'] == id)['semester']
    document.getElementById('updateformdiv').style.display = 'flex';
    studentIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let studentnametoupdate = document.getElementById('studentnametoupdate').value;
    let semestertoupdate = document.getElementById('semestertoupdate').value;
    fetch('http://localhost:48224/student', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                studentId: studentIdToUpdate,
                studentName: studentnametoupdate,
                semester: semestertoupdate
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
    fetch('http://localhost:48224/student/'+id, {
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
    let name = document.getElementById('studentname').value;
    let semesterFromForm = document.getElementById('semester').value;
    fetch('http://localhost:48224/student', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                studentName: name,
                semester: semesterFromForm
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error', error); });
    
}