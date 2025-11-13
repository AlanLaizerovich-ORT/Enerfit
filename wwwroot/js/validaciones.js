document.addEventListener("DOMContentLoaded", function () {
    const loginForm = document.getElementById("loginForm");

    if (loginForm) {
        loginForm.addEventListener("submit", function (event) {
            const nombreUsuario = document.getElementById("nombreUsuario").value.trim();
            const contrasenia = document.getElementById("contrasenia").value.trim();

            if (!nombreUsuario || !contrasenia) {
                event.preventDefault(); // Evita enviar el formulario si hay campos vacíos
                alert("Por favor, complete todos los campos.");
                return;
            }
        });
    }

    const registroForm = document.getElementById("registroForm");

    if (registroForm) {
        registroForm.addEventListener("submit", function (event) {
            const nombreUsuario = document.getElementById("nombreUsuario").value.trim();
            const contrasenia = document.getElementById("contrasenia").value.trim();
            const email = document.getElementById("email").value.trim();

            if (!nombreUsuario || !contrasenia || !email) {
                event.preventDefault();
                alert("Por favor, complete todos los campos obligatorios.");
                return;
            }

            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                event.preventDefault();
                alert("Por favor, ingrese un email válido.");
                return;
            }
        });
    }
});
