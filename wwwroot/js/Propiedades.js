document.addEventListener("DOMContentLoaded", () => {
    // URL de la imagen por defecto
    const defaultImage = "/images/default-property.jpg"; // Cambia esto a la ruta real de tu imagen por defecto

    // Selecciona todas las imágenes con la clase "property-image"
    const images = document.querySelectorAll(".property-image");

    images.forEach((img) => {
        const imageUrl = img.getAttribute("data-imagen");

        // Si no hay imagen definida, usar la imagen por defecto
        if (!imageUrl || imageUrl.trim() === "") {
            img.src = defaultImage;
        } else {
            // Verifica si la imagen existe en el servidor
            const imgTest = new Image();
            imgTest.src = imageUrl;

            imgTest.onload = () => {
                // La imagen existe, se establece como fuente
                img.src = imageUrl;
            };

            imgTest.onerror = () => {
                // La imagen no existe, usar la imagen por defecto
                img.src = defaultImage;
            };
        }
    });
});
