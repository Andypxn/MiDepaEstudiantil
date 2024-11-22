function deleteProperty(button, propertyId) {
    const confirmation = confirm("¿Estás seguro de que deseas eliminar esta propiedad?");
    if (confirmation) {
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        fetch(`/MisPropiedades?handler=Delete&id=${propertyId}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            }
        })
            .then(response => {
                if (response.ok) {
                    button.closest('.property-item').remove();
                    alert("La propiedad ha sido eliminada con éxito.");
                } else {
                    alert("Hubo un error al eliminar la propiedad.");
                }
            })
            .catch(error => {
                alert("Error al eliminar la propiedad. Por favor, inténtelo nuevamente.");
                console.error("Error:", error);
            });
    } else {
        alert("No se eliminó la propiedad.");
    }
}

function editProperty(button, propertyId) {
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    fetch(`/MisPropiedades?handler=Edit&id=${propertyId}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": token
        }
    })
        .then(response => {
            if (response.ok) {
                button.closest('.property-item').remove();
                alert("La propiedad ha sido editada con éxito.");
            } else {
                alert("Hubo un error al eliminar la propiedad.");
            }
        })
        .catch(error => {
            alert("Error al eliminar la propiedad. Por favor, inténtelo nuevamente.");
            console.error("Error:", error);
        });
}