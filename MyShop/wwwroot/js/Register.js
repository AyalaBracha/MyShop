const progress = document.querySelector("#progress")
const message = document.getElementById("message");
const message1 = document.getElementById("message1");
const message2 = document.getElementById("message2");

const toRegister = () => {
    const container = document.querySelector(".container")
    container.classList.remove("container")

}


function isValidEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}





const getDataRfomForm = () => {
    message1.textContent = "";
    message.textContent = "";
    const user =
    {
        email: document.querySelector("#email").value,
        firstName: document.querySelector("#firstName").value,
        lastName: document.querySelector("#lastName").value,
        password: document.querySelector("#password").value
    }
    if (!isValidEmail(user.email)) {
        message.textContent = "The email is invalid.";
        message.style.color = "red";
        return null;
    }
    if (user.firstName.length > 8 || user.firstName.length < 4) {
        message1.textContent = "The firstName can be between 4 till 8.";
        message1.style.color = "red";
        return null;
    }
    if (progress.value < 3) {
        message1.textContent = "The password so week😢😢😢😢😢😙😐😑😪😥";
        message1.style.color = "red";
        return null;
    }
    return user;
}
const getEmailAndPasswordFromForm = () => {

    email = document.querySelector("#Email").value,
        password = document.querySelector("#Password").value
    return { email, password }
}

const createUser = async () => {

    const newUser = getDataRfomForm()

    if (newUser != null) {
        try {
            const responsePost = await fetch("https://localhost:44376/api/Users", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUser)

            });
            if (!responsePost.ok) {
                throw new Error(`HTTP error! status:${response.status}`)
            }

            const dataPost = await responsePost.json();

            alert(`${dataPost} נרשמת בהצלחה `)



        }
        catch (error) {
            console.log(error)
        }
    }
}
const login = async () => {
    const { email, password } = getEmailAndPasswordFromForm()
    try {
        const responsePost = await fetch(`api/Users/login?email=${email}&password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                email: email,
                password: password
            }
        });
        if (responsePost.status == 204)
            alert("not found")
        if (!responsePost.ok) {
            throw new Error(`HTTP error! status:${response.status}`)
        }
        const dataPost = await responsePost.json();
        console.log(dataPost)
        sessionStorage.setItem("User", dataPost.id)
        window.location.href = "ShoppingBag.html"
    }
    catch (error) {
        alert("משתמש לא קיים")
    }
}
const checkPassword = async () => {

    const password = document.querySelector("#password").value;
    try {
        const response = await fetch(`api/Users/checkPassword`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });
        const data = await response.json()
        progress.value = data + 1
    }
    catch (error) {
        console.log(error)
    }



}
const updateUserDetails = async () => {
    const newUser = getDataRfomForm();
    const userId = sessionStorage.getItem("User")
    //alert(userId)
    //alert(JSON.stringify(newUser))
    if (newUser != null) {
        try {
            const responsePost = await fetch(`api/Users/${userId}`, {
                method: 'Put',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newUser)

            });
            if (!responsePost.ok) {
                throw new Error(`HTTP error! status:${response.status}`)
            }

            const dataPost = await responsePost.json();
            alert("עודכן בהצלחה!!😂😋😍🥰😄😃")
            window.location.href = 'ShoppingBag.html'
            


        }
        catch (error) {
            console.log(error)
        }
    }
}






