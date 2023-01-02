const payAmountBtn = document.querySelector('#payAmount');
const decrementBtn = document.querySelectorAll('#decrement');
const quantityElem = document.querySelectorAll('#quantity');
const incrementBtn = document.querySelectorAll('#increment');
const priceElem = document.querySelectorAll('#price');
const totalElem = document.querySelector('#total');
const subtotalElem = document.querySelector('#subtotal');
const shippingElm = document.querySelector('#shipping');
const discountElm = document.querySelector('#discount-token');

for (let i = 0; i < incrementBtn.length; i++) {

    incrementBtn[i].addEventListener('click', function () {

       
        let increment = Number(this.previousElementSibling.textContent);

        increment++;

        this.previousElementSibling.textContent = increment;

        totalCalc();

    });

    decrementBtn[i].addEventListener('click', function () {

  
        let decrement = Number(this.nextElementSibling.textContent);

       
        decrement <= 1 ? 1 : decrement--;

        this.nextElementSibling.textContent = decrement;

        totalCalc();

    });

}


const totalCalc = function () {

 
    const shipping = 50;
    let subtotal = 0;
    let total = 0;

 
    for (let i = 0; i < quantityElem.length; i++) {

        subtotal += Number(quantityElem[i].textContent) * Number(priceElem[i].textContent);
        console.log(Number(priceElem[i].textContent));

    }


    subtotalElem.textContent = subtotal;

    shippingElm.textContent = shipping;

  
    total = subtotal + shipping;


    totalElem.textContent = total;
    payAmountBtn.textContent = total;

}

var elements = document.getElementsByClassName("method");
for (var i = 0; i < elements.length; i++) {
    elements[i].onclick = function () {

        var el = elements[0];
        while (el) {
            if (el.tagName === "BUTTON") {
                el.classList.remove("choose");

            }
            el = el.nextSibling;
        }
        this.classList.add("choose");
    };
}