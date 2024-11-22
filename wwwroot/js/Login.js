// Mostrar y ocultar contraseña
function togglePasswordVisibility(passwordFieldId, toggleButtonId) {
    const passwordField = document.getElementById(passwordFieldId);
    const toggleButton = document.getElementById(toggleButtonId);

    toggleButton.addEventListener("click", function () {
        const isPasswordVisible = passwordField.type === "text";
        passwordField.type = isPasswordVisible ? "password" : "text";
        toggleButton.textContent = isPasswordVisible ? "Mostrar" : "Ocultar";
    });
}

// Inicializa el script
document.addEventListener("DOMContentLoaded", function () {
    togglePasswordVisibility("contrasena", "togglePassword");
});
        