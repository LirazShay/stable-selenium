createButtonHiddenUnderAnotherDiv();
function createButtonHiddenUnderAnotherDiv() {
    document.body.innerHTML = '';
    var btn = document.createElement('button');
    btn.textContent = 'Click me';
    btn.style.position = 'absolute';
    btn.style.top = '0';
    btn.style.left = '0';
    btn.onclick = function () {
        document.body.append('button clicked');
        document.body.appendChild(document.createElement('br'));
    }
    document.body.appendChild(btn);
    var div = document.createElement('div');
    div.id = 'hideBtnDiv';
    div.textContent = 'I am hiding the button';
    div.style.width = '100px';
    div.style.backgroundColor = 'red';
    div.style.position = 'absolute';
    div.style.top = '0';
    div.style.left = '0';
    document.body.appendChild(div);
}