const progress = document.querySelector("#progress")
const toRegister = () => {
    const hid = document.querySelector(".hid")
    hid.classList.remove("hid")
   
}

const getDataRfomForm = () => {
    
    const user = 
    {
        email: document.querySelector("#email").value,
        firstName: document.querySelector("#firstName").value,
        lastName: document.querySelector("#lastName").value,
        password: document.querySelector("#password").value
    }
    return user;
}
const getEmailAndPasswordFromForm = () => {

    email=document.querySelector("#Email").value,
    password = document.querySelector("#Password").value
    return {email,password}
}

const createUser = async () => {
   
    const newUser = getDataRfomForm()
    try {
        const responsePost = await fetch("https://localhost:44376/api/Users", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)

        });
        //if status==400
        //weak password
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
        sessionStorage.setItem("User", dataPost.userId)

        window.location.href = "UserDetails.html"
    }
    catch (error) {
        alert("משתמש לא קיים")
    }
}
const checkPassword =async () => {
 
    const password = document.querySelector("#password").value;
    try { 
        const response = await fetch(`api/Users/checkPassword`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)});
            const data = await response.json()
        progress.value = data + 1//why +1?
        }
    catch(error) {
       console.log(error)
        }
        

    
}
const updateUserDetails = async () => {
    const newUser = getDataRfomForm();
    const userId = sessionStorage.getItem("User")
    alert(userId)
    alert(JSON.stringify(newUser))
    try {
        const responsePost = await fetch(`api/Users/${userId}`, {
            method: 'Put',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)

        });
        //if status==400
        //weak password
        if (!responsePost.ok) {
            throw new Error(`HTTP error! status:${response.status}`)
        }

        const dataPost = await responsePost.json();
        sessionStorage.setItem("User", dataPost.userId);


    }
    catch (error) {
        console.log(error)
    }
}






