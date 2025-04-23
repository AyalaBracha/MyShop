const message = document.getElementById("message");
const message1 = document.getElementById("message1");
const message2 = document.getElementById("message2");


const getDataRfomForm = () => {

    const newUser =
    {
        email: document.querySelector("#email").value,
        firstName: document.querySelector("#firstName").value,
        lastName: document.querySelector("#lastName").value,
        password: document.querySelector("#password").value
    }
    return newUser
}

const updateUserDetails =async () => {
    const newUser = getDataRfomForm();
    const Id =sessionStorage.getItem("User")
    //alert(Id)
    alert(JSON.stringify(newUser))
    try {
        const responsePost = await fetch(`api/Users/${Id}`, {
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
        sessionStorage.setItem("User", dataPost.Id);
       

    }
    catch (error) {
        console.log(error)
    }
}