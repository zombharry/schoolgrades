let subjects = [];
let connection = null;

let subjectIdToUpdate = -1;
getData();
setupSignalR();
function setupSignalR()
{
    connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:48224/hub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("SubjectCreated", (user, message) => {
        getData();
    });

    connection.on("SubjectDeleted", (user, message) => {
        getData();
    });
    connection.on("SubjectUpdated", (user, message) => {
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
    await fetch('http://localhost:48224/subject')
        .then(x => x.json())
        .then(y => {
            subjects = y;
            console.log(y);

            display();
            
        });
}



function display()
{
    document.getElementById('resultArea').innerHTML = "";
    subjects.forEach(t => {
        document.getElementById('resultArea').innerHTML +=
            `<tr>
                <td>${t.subjectId}</td>
                <td>${t.subjectName} </td>
                <td>${t.credit} </td>
                <td>
                    <button onclick="remove(${t.subjectId})">Delete</button>
                    <button onclick="showUpdate(${t.subjectId})">Edit</button>
                 </td>
            </tr>`;
    })
}
function showUpdate(id)
{

    document.getElementById('subjectnametoupdate').value = subjects.find(t => t['subjectId'] == id)['subjectName']
    document.getElementById('credittoupdate').value = subjects.find(t => t['subjectId'] == id)['credit']
    document.getElementById('updateformdiv').style.display = 'flex';
    subjectIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let subjectnametoupdate = document.getElementById('subjectnametoupdate').value;
    let credittoupdate = document.getElementById('credittoupdate').value;
    fetch('http://localhost:48224/subject', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                subjectId: subjectIdToUpdate,
                subjectName: subjectnametoupdate,
                credit: credittoupdate
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
    fetch('http://localhost:48224/subject/'+id, {
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
    let name = document.getElementById('subjectname').value;
    let creditFromForm = document.getElementById('credit').value;
    fetch('http://localhost:48224/subject', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                subjectName: name,
                credit: creditFromForm
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getData();
        })
        .catch((error) => { console.error('Error', error); });
    
}