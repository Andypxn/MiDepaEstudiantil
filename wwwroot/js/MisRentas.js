function registrarArrendatario(id) {
    const telefono = document.getElementById(`telefono-${id}`).value;

    if (!telefono || telefono.length !== 10) {
        alert("Por favor, introduce un número de teléfono válido.");
        return;
    }

    fetch(`/MisRentas?handler=Registrar&id=${id}&telefono=${telefono}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    })
    .then(response => {
        if (response.ok) {
            alert("Propiedad marcada como ocupada.");
            location.reload();
        } else {
            alert("Error al registrar la propiedad.");
        }
    });
}

function desvincularPropiedad(id) {
    if (confirm("¿Estás seguro de desvincular esta propiedad?")) {
        fetch(`/MisRentas?handler=Desvincular&id=${id}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]').value
            }
        })
        .then(response => {
            if (response.ok) {
                alert("Propiedad marcada como disponible.");
                location.reload();
            } else {
                alert("Error al desvincular la propiedad.");
            }
        });
    }
}
