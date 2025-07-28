addEventListener("load", () => {
    let product = JSON.parse(sessionStorage.getItem("shopingBag")) || []
    let category =  []
    sessionStorage.setItem("category", JSON.stringify(category))
    sessionStorage.setItem("shopingBag", JSON.stringify(product))
    document.querySelector("#ItemsCountText").textContent = product.length
    filterProducts()
    drawCategory()//יותר נכון לעשות ההפך- לקרוא לפונקציה ששולפת את הקטגוריות מהשרת והיא תקרא תשלח לפונקציה שמציירת אותם על המסך
    //כמו שעשית עם המוצרים
} )
const getData = async() => {

    let search =
    {
        nameSearch: document.querySelector("#nameSearch").value,
        minPrice: parseInt(document.querySelector("#minPrice").value),
        maxPrice: parseInt(document.querySelector("#maxPrice").value),
        categoryIds : sessionStorage.getItem("category")
      
    }
    return search  
}
   
    const filterProducts = async () => {
        let { nameSearch, minPrice, maxPrice, categoryIds } = await getData()
        categoryIds = JSON.parse(categoryIds)
        console.log(categoryIds);
        let url = `api/products/`;
        if (nameSearch || maxPrice || minPrice || categoryIds.length!=0) { 
            url += '?'
        if (nameSearch)
            url += `&desc=${nameSearch}`
        if (maxPrice)
            url += `&maxPrice=${maxPrice}`
        if (minPrice)
                url += `&minPrice=${minPrice}`
            if (categoryIds.length != 0)
                for (let i = 0; i < categoryIds.length;i++)
                url += `&categoryIds=${categoryIds[i]}`
        }
        try {
            const responsePost = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },
                query: {
                    nameSearch: nameSearch,
                    maxPrice: maxPrice,
                    minPrice: minPrice,
                    categoryIds: categoryIds
                }
            });
            if (responsePost.status == 204)
                alert("not found products")
            if (!responsePost.ok) {
                throw new Error(`HTTP error! status:${response.status}`)
            }
            const dataPost = await responsePost.json();
            console.log(dataPost);
            document.getElementById("counter").textContent = dataPost.length
            console.log(document.getElementById("counter").textContent);
            drawProducts(dataPost);
        }
        catch (error) {
            
            alert("יש לכם שגיאה")
        }
        
}
const drawTemplate =(product) => {
   
    let tmp = document.getElementById("temp-card");
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector("img").src = "./pictures/" + product.image
    cloneProduct.querySelector("h1").textContent = product.productName
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.descreaptionProduct
    cloneProduct.querySelector(".bag").addEventListener('click', () => { addToCart(product) })
    document.getElementById("PoductList").appendChild(cloneProduct)
}
const drawProducts = async (products) => {
    document.getElementById("PoductList").innerHTML = ""
    for (let i = 0; i < products.length; i++) {
        drawTemplate(products[i])
    }
}

const addToCart = (product) => {
        let products = sessionStorage.getItem("shopingBag")
        products = JSON.parse(products)
    products.push(product)
    sessionStorage.setItem("shopingBag", JSON.stringify(products))
    document.querySelector("#ItemsCountText").textContent = parseInt(document.querySelector("#ItemsCountText").textContent)+1
    }
    const addCategory = async () => {//? it's not add it's get
        try {
            const responsePost = await fetch('api/categories', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                },

            });
            if (responsePost.status == 204)
                alert("not found categories")
            if (!responsePost.ok) {
                throw new Error(`HTTP error! status:${response.status}`)
            }
            const dataPost = await responsePost.json();
            console.log(dataPost);
            return(dataPost);
            
        }
        catch (error) {

            alert("יש לכם שגיאה")
        }

       
        
}
const drawCategory = async () => {
    category = await addCategory()
    for (let i = 0; i < category?.length; i++) {
        let tmp = document.getElementById("temp-category");
        let cloneCategory = tmp.content.cloneNode(true)
        cloneCategory.querySelector("input").id=i
        cloneCategory.querySelector("input").addEventListener('change', () => { filterCategory(category[i],i) }) 
        cloneCategory.querySelector("label").textContent = category[i].name

        document.getElementById("categoryList").appendChild(cloneCategory)
    }
}
const filterCategory=(category,index)=>{
    const input = document.getElementById(index)
   
    if (input.checked) {
        let categories = sessionStorage.getItem("category")
        categories = JSON.parse(categories)
        categories.push(category.id)
        sessionStorage.setItem("category", JSON.stringify(categories))
    }
    else {
        let categories = sessionStorage.getItem("category")
        categories = JSON.parse(categories)
        //use indexOf instead of for
        let i = 0;
        for (; i < categories.length; i++) {
            if (categories[i] == category.id)
                break;
        }
        categories.splice(i, 1);
        sessionStorage.setItem("category", JSON.stringify(categories))
    }
    filterProducts()
}
