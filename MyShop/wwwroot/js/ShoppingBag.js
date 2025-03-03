addEventListener("load", () => {
    let user = JSON.parse(sessionStorage.getItem("User")) || []
    sessionStorage.setItem("User", JSON.stringify(user))
    drawProducts() 
})
const drawTemplate = (product) => {
   
    let tmp = document.getElementById("temp-row");
    let cloneProduct = tmp.content.cloneNode(true)
    let url = `./pictures/${product.image}`
   
    cloneProduct.querySelector(".image").style.backgroundImage = `url(${url})`
    /*cloneProduct.querySelector(".img").src = "./pictures/" + product.image*/
    cloneProduct.querySelector(".itemName").textContent = product.productName
    cloneProduct.querySelector(".itemNumber").innerText = product.price
    //cloneProduct.querySelector(".description").innerText = product.descreaptionProduct
    cloneProduct.querySelector(".expandoHeight").addEventListener('click', () => { deleteFromCart(product) })
    document.querySelector("tbody").appendChild(cloneProduct)
}
const drawProducts = async () => {
  
    products = sessionStorage.getItem("shopingBag")
    products = JSON.parse(products)
    let price = 0;
    document.getElementById("itemCount").textContent = products?.length; 
   
    document.querySelector("tbody").innerHTML=''
    for (let i = 0; i < products.length; i++) {
        price +=parseInt( products[i].price)
        drawTemplate(products[i])
    }
    document.getElementById("totalAmount").textContent = price+'$';
}
const deleteFromCart = (product) => {
    let products = sessionStorage.getItem("shopingBag")
    products = JSON.parse(products)
    let i = 0;
    for (; i < products.length; i++) {
        if (products[i].id == product.id)
            break;
    }
    products.splice(i, 1);
    sessionStorage.setItem("shopingBag", JSON.stringify(products))
    drawProducts()
}
const generateDate = () => {
    const date = new Date();

    let day = date.getDate();
    let month = date.getMonth() + 1;
    let year = date.getFullYear();


    let currentDate = year+"-"+month+"-"+day;

    console.log(currentDate);
    return currentDate
}
const placeOrder =async () => {
    shopingBag = JSON.parse(sessionStorage.getItem("shopingBag")) || []
    console.log(shopingBag)
    let user = JSON.parse(sessionStorage.getItem("User")) || []
    if (user.length != 0 && shopingBag.length != 0) {
        let products = []
        let sum = 0;
        for (let i = 0; i < shopingBag.length;i++) {
            let currentProduct = { productId: shopingBag[i].id, quentity: 1, price: shopingBag[i].price }
            products.push(currentProduct);
            sum += parseFloat(currentProduct.price);
            console.log(sum)
            console.log(currentProduct)
        }
        try {
            
            const responsePost = await fetch("https://localhost:44376/api/Orders", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify( {
                userId: user,
                date:"2025-01-05",
                orderItems: products,
                sum:sum
                })

            });
            if (!responsePost.ok) {
                throw new Error(`HTTP error! status:${response.status}`)
            }

            const dataPost = await responsePost.json();

            alert(`${dataPost.id} הזמנה בוצעה בהצלחה `)
            sessionStorage.setItem("shopingBag", JSON.stringify([]))
            sessionStorage.setItem("category", JSON.stringify([]))
            window.location.href = 'Products.html'


        }
        catch (error) {
            console.log(error)
        }
    }
    else {
        window.location.href='Home.html'
     }
    generateDate()
}