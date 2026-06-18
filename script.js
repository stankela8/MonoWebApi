const playerForm = document.getElementById("playerForm");
const playerIdInput = document.getElementById("playerId");

const nameInput = document.getElementById("name");
const positionInput = document.getElementById("position");
const clubInput = document.getElementById("club");
const shirtNumberInput = document.getElementById("shirtNumber");
const ageInput = document.getElementById("age");

const tableBody = document.getElementById("tableBody");
const addBtn = document.getElementById("addBtn");
const saveBtn = document.getElementById("saveBtn");
const refreshBtn = document.getElementById("refreshBtn");

const storageKey = "players";

let players = [];

//spremanje i dohvaćanje podataka iz localStoragea
function savePlayersToLocalStorage() {
    localStorage.setItem(storageKey, JSON.stringify(players));
}

function loadPlayersFromLocalStorage() {
    const data = localStorage.getItem(storageKey);

    if (data) {
        players = JSON.parse(data);
    } else {
        players = [];
    }
}
//prikaz tablice
function showTable() {
    loadPlayersFromLocalStorage();
    tableBody.innerHTML = "";

    for (let player of players) {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${player.name}</td>
            <td>${player.position}</td>
            <td>${player.club}</td>
            <td>${player.shirtNumber}</td>
            <td>${player.age}</td>
            <td>
                <button type="button" onclick="editPlayer(${player.id})">Edit</button>
                <button type="button" onclick="deletePlayer(${player.id})">Delete</button>
            </td>
        `;

        tableBody.appendChild(row);
    }
}
//brisanje vrijednosti iz forme, ponavlja se
function clearForm() {
    playerIdInput.value = "";
    nameInput.value = "";
    positionInput.value = "";
    clubInput.value = "";
    shirtNumberInput.value = "";
    ageInput.value = "";
}

addBtn.addEventListener("click", function () {
    clearForm();
    playerForm.style.display = "block";
});

function createPlayer() {
    loadPlayersFromLocalStorage();

    const newPlayer = {
        id: Date.now(),
        name: nameInput.value,
        position: positionInput.value,
        club: clubInput.value,
        shirtNumber: Number(shirtNumberInput.value),
        age: Number(ageInput.value)
    };

    players.push(newPlayer);
    savePlayersToLocalStorage();
    showTable();
}

function editPlayer(id) {
    loadPlayersFromLocalStorage();

    const player = players.find(x => x.id === id);

    if (!player) return;

    playerForm.style.display = "block";
    playerIdInput.value = player.id;
    nameInput.value = player.name;
    positionInput.value = player.position;
    clubInput.value = player.club;
    shirtNumberInput.value = player.shirtNumber;
    ageInput.value = player.age;
}

function updatePlayer() {
    loadPlayersFromLocalStorage();

    const id = Number(playerIdInput.value);
    const player = players.find(x => x.id === id);

    if (!player) return;

    player.name = nameInput.value;
    player.position = positionInput.value;
    player.club = clubInput.value;
    player.shirtNumber = Number(shirtNumberInput.value);
    player.age = Number(ageInput.value);

    savePlayersToLocalStorage();
    showTable();
}

function deletePlayer(id) {
    loadPlayersFromLocalStorage();

    players = players.filter(x => x.id !== id);

    savePlayersToLocalStorage();
    showTable();
}

saveBtn.addEventListener("click", function () {
    if (playerIdInput.value === "") {
        createPlayer();
    } else {
        updatePlayer();
    }

    playerForm.style.display = "none";
    clearForm();
});

refreshBtn.addEventListener("click", function () {
    showTable();
});

showTable();