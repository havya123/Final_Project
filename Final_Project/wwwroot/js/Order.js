const payAmountBtn = document.querySelector('#payAmount');
const decrementBtn = document.querySelectorAll('#decrement');
const quantityElem = document.querySelectorAll('#quantity');
const incrementBtn = document.querySelectorAll('#increment');
const priceElem = document.querySelectorAll('#price');
const totalElem = document.querySelector('#total');
const subtotalElem = document.querySelector('#subtotal');
const shippingElm = document.querySelector('#shipping');

// loop: for add event on multiple `increment` & `decrement` button
for (let i = 0; i < incrementBtn.length; i++) {

    incrementBtn[i].addEventListener('click', function () {

        // collect the value of `quantity` textContent,
        // based on clicked `increment` button sibling. 
        let increment = Number(this.previousElementSibling.textContent);

        // plus `increment` variable value by 1
        increment++;

        // show the `increment` variable value on `quantity` element
        // based on clicked `increment` button sibling.
        this.previousElementSibling.textContent = increment;

        totalCalc();

    });

    decrementBtn[i].addEventListener('click', function () {

        // collect the value of `quantity` textContent,
        // based on clicked `decrement` button sibling.
        let decrement = Number(this.nextElementSibling.textContent);

        // minus `decrement` variable value by 1 based on condition
        decrement <= 1 ? 1 : decrement--;

        // show the `decrement` variable value on `quantity` element
        // based on clicked `decrement` button sibling.
        this.nextElementSibling.textContent = decrement;

        totalCalc();

    });

}


const totalCalc = function () {
    
    // declare all initial variable
    const shipping = 50;
    let subtotal = 0; 
    let total = 0;

    // loop: for calculating `subtotal` value from every single product
    for (let i = 0; i < quantityElem.length; i++) {

        subtotal += Number(quantityElem[i].textContent) * Number(priceElem[i].textContent);
        console.log(Number(priceElem[i].textContent));

    }

    // show the `subtotal` variable value on `subtotalElem` element
    subtotalElem.textContent = subtotal;

    shippingElm.textContent = shipping;

    // calcualting the `total`
    total = subtotal + shipping;
    console.log(total);

    // show the `total` variable value on `totalElem` & `payAmountBtn` element
    totalElem.textContent = total;
    payAmountBtn.textContent = total;

}

var elements = document.getElementsByClassName("method");
for (var i = 0; i < elements.length; i++) {
    elements[i].onclick = function () {

        // remove class from sibling

        var el = elements[0];
        while (el) {
            if (el.tagName === "BUTTON") {
                //remove class
                el.classList.remove("choose");

            }
            // pass to the new sibling
            el = el.nextSibling;
        }

        this.classList.add("choose");
    };
}
