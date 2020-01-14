
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function isNumberOrDotKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    var source = event.target || event.srcElement;
    if (charCode == 46) {
        if (source.value.indexOf(".") > 0) { //Allow only one dot (.)
            return false;
        }
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function RoundOffNumber(evt, digitsAfterDecimal) {
    var source = event.target || event.srcElement;
    if (!isNaN(source.value)) {
        var n = source.value.indexOf('.');
        if (n > -1) {
            if (source.value.substring(n).length > digitsAfterDecimal + 1) {
                alert('value will be rounded off upto ' + digitsAfterDecimal + ' digits after decimal');
                var num = parseFloat(source.value);
                source.value = num.toFixed(digitsAfterDecimal);
            }
        }
    }
    return true;
}