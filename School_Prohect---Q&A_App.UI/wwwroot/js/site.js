
function FormatDate(d) {
    var date = new Date(d);
    var day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate();
    var mount = date.getMonth() > 9 ? date.getMonth() : '0' + date.getMonth();
    var year = date.getFullYear();

    var hour = date.getHours() > 9 ? date.getHours() : '0' + date.getHours();
    var minute = date.getMinutes() > 9 ? date.getMinutes() : '0' + date.getMinutes();
    var second = date.getSeconds() > 9 ? date.getSeconds() : '0' + date.getSeconds();

    var fd = day + "." + mount + "." + year + " " + hour + ":" + minute + ":" + second;
    return fd
}