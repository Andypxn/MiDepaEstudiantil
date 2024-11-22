document.getElementById("logoutButton").addEventListener("click", function () {
    if (confirm("¿Seguro que deseas cerrar sesión?")) {
        fetch('/api/auth/logout', {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                "Content-Type": "application/json"
            }
        })
        .then(response => {
            if (response.ok) {
                window.location.href = "/Login"; // Redirigir a la página de inicio de sesión
            } else {
                console.error("Error al cerrar sesión. Código de estado:", response.status);
                alert("Error al cerrar sesión. Inténtalo de nuevo.");
            }
        })
        .catch(error => console.error('Error:', error));
    }
});
