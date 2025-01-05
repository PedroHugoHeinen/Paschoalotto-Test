const apiUrl = 'https://localhost:7287/PaschoalottoApi/User';

async function displayAllUsers() {
    const response = await fetch(`${apiUrl}/GetAll`);
    const users = await response.json();

    const usersTableBody = document.getElementById("usersTable").getElementsByTagName("tbody")[0];
    usersTableBody.innerHTML = "";

    users.forEach((user, index) => {
        const row = usersTableBody.insertRow();

        const idCell = row.insertCell(0);
        idCell.textContent = user.id;

        const nameCell = row.insertCell(1);
        nameCell.textContent = `${user.firstName} ${user.lastName}`;

        const emailCell = row.insertCell(2);
        emailCell.textContent = user.email;

        const phoneCell = row.insertCell(3);
        phoneCell.textContent = user.phone;

        const statusCell = row.insertCell(4);
        statusCell.textContent = (user.status === true? 'Ativo' : 'Inativo');

        const actionsCell = row.insertCell(5);
        actionsCell.innerHTML = `
            <button class="btn btn-secondary btn-sm" onclick="editUser('${user.id}')">Editar</button>
            <button class="btn btn-warning btn-sm" onclick="deleteUser('${user.id}')">Excluir</button>
        `;
    });
}

document.getElementById("userForm").addEventListener("submit", async function (event) {
    event.preventDefault();

    const id = document.getElementById("id").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    const email = document.getElementById("email").value;
    const phone = document.getElementById("phone").value;
    const gender = document.getElementById("gender").value;
    const age = document.getElementById("age").value;
    const status = document.getElementById("status").checked;
    const createAt = document.getElementById("createAt").value;
    const lastUpdateAt = document.getElementById("lastUpdateAt").value;


    const userDTO =
    {
        id,
        firstName,
        lastName,
        email,
        phone,
        gender,
        age,
        status,
        createAt,
        lastUpdateAt
    };

    if (id) {
        const confirmAction = await confirmActionAsync('Atenção', 'Tem certeza que salvar as alterações?');

        if (!confirmAction) { return; }

        userDTO.lastUpdateAt = userDTO.lastUpdateAt || null;

        await fetch(`${apiUrl}/Update`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userDTO)
        });
    } else {
        userDTO.id = 0;
        userDTO.createAt = new Date();
        userDTO.lastUpdateAt = null;

        await fetch(`${apiUrl}/Insert`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userDTO)
        });
    }

    displayAllUsers();
    this.reset();
    document.getElementById("id").value = "";
});

async function editUser(id) {
    const response = await fetch(`${apiUrl}/GetById/${id}`);
    const user = await response.json();

    document.getElementById("id").value = user.id;
    document.getElementById("firstName").value = user.firstName;
    document.getElementById("lastName").value = user.lastName;
    document.getElementById("email").value = user.email;
    document.getElementById("phone").value = user.phone;
    document.getElementById("gender").value = user.gender;
    document.getElementById("age").value = user.age;
    document.getElementById("status").checked = user.status;
    document.getElementById("createAt").value = user.createAt;
    document.getElementById("lastUpdateAt").value = user.lastUpdateAt;
}

async function deleteUser(id) {
    const confirmAction = await confirmActionAsync('Atenção', 'Tem certeza que deseja excluir o usuário?');

    if (!confirmAction) { return; }

    await fetch(`${apiUrl}/Delete/${id}`, {
        method: 'DELETE'
    });

    displayAllUsers();
}

document.getElementById("cancelEdit").addEventListener("click", function () {
    document.getElementById("userForm").reset();
    document.getElementById("id").value = "";
});

document.getElementById("getReport").addEventListener("click", async function () {
    
});

document.getElementById("insertRandom").addEventListener("click", async function () {
    await fetch(`${apiUrl}/InsertRandom`);

    displayAllUsers();
});

document.getElementById("refreshList").addEventListener("click", async function () {
    displayAllUsers();
});

async function confirmActionAsync(title, message) {
    const result = await Swal.fire({
        title: title,
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#5a6268',
        cancelButtonColor: '#ffc107',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não'
    });

    return result.isConfirmed;
}

displayAllUsers();