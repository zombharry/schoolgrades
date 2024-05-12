let students = [];
let connection = null;
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
                    <button>Edit</button>
                 </td>
            </tr>`;
    })
}
function remove(id)
{
    alert(id);
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