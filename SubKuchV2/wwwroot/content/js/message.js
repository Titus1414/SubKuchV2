"use strict";





var connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalServer", {
        accessTokenFactory: () => "testing"

                        })
                        .build();

connection.on("ReceiveMessage", function(orderid) {
   // var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
  //  var div = document.createElement("div");
    GetOrder(orderid);
    var mydiv = document.getElementById("notificationdiv");
    var anchor = document.createElement("a");
    anchor.setAttribute("class", "dropdown-item notify-item border-bottom");
    var para = document.createElement("p");
    para.setAttribute("class","notify-details")

    para.innerHTML = "There's a new order for Order Number" + orderid;
    anchor.appendChild(para);
    var notsound = document.getElementById("myAudio");
    notsound.play();
  
    mydiv.appendChild(anchor);
  
});
connection.on("Inprogress", function (orderid) {
  
    Inprogressorder(orderid);
    RiderOrderAccepted(orderid);
    var notsound = document.getElementById("myAudio");
    notsound.play();
});
connection.on("OrderPicked", function (orderid) {

    Inprogressorder(orderid);
    RiderOrderAccepted(orderid);
    var notsound = document.getElementById("myAudio");
    notsound.play();
});
connection.on("RiderRequest", function (orderid) {
   
    RiderOrderRequest(orderid)
    var notsound = document.getElementById("myAudio");
    notsound.play();
});
connection.on("OrderCompleted", function (orderid) {

    OrderCompleted(orderid)
    var notsound = document.getElementById("myAudio");
    notsound.play();
});
connection.on("OrderPicked", function (orderid) {

    OrderPicked(orderid)
    var notsound = document.getElementById("myAudio");
    notsound.play();
});
function GetOrder(id) {
 

    $.ajax({
        type: 'POST',
        url: '/OrderBasic/GetOrder',
        data: { 'orderid': id },
        success: function (response) {

         
            var detailitem = "";
            $.each(response.orderdetails, function (index, value) {
                detailitem += value.dishname + ',';
            });
         

            var dataaaa = document.getElementById("neworder");
            var row = dataaaa.insertRow(0);
            row.id = id;
            row.setAttribute("style", "background-color:yellow;");
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            var cell4 = row.insertCell(3);
            var cell5 = row.insertCell(4);
            var cell6 = row.insertCell(5);
            var cell7 = row.insertCell(6);
            var cell8 = row.insertCell(7);
            var cell9 = row.insertCell(8);
            var cell10 = row.insertCell(9);
          //  cell9.id = id + response.status;
            cell1.innerHTML = '#'+response.orderCode;
            cell2.innerHTML = detailitem;
            cell3.innerHTML = response.consumerName;
            cell4.innerHTML = response.citchenName;
            cell5.innerHTML = response.citchenPhone;

            cell6.innerHTML = response.consumerPhone;
            cell7.innerHTML = response.orderBilling;
            cell8.innerHTML = response.datee;
            cell9.innerHTML = response.status;
            cell10.innerHTML = '<a href="@Url.Action("Accept","OrderBasic",new {Id=item.OrderInfoVieModel.OrderId,Status="InProcess" })" class="btn btn-primary">Accept </a>';
         
           
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function Inprogressorder(id) {
    var getrow = document.getElementById(id);
    getrow.cells[8].innerHTML = "InProgress";
    getrow.setAttribute("style", "background-color:green;color:white;");

        
}
function RiderOrderRequest(id) {
    $.ajax({
        type: 'POST',
        url: '/OrderBasic/GetRiderRequest',
        data: { 'orderid': id },
        success: function (response) {




            var dataaaa = document.getElementById("riderRequests");
            var row = dataaaa.insertRow(0);
            row.id = "rider"+id;
            row.setAttribute("style", "background-color:yellow;");
            var cell1 = row.insertCell(0);
            var cell2 = row.insertCell(1);
            var cell3 = row.insertCell(2);
            var cell4 = row.insertCell(3);

            //console.log(response);
            cell1.innerHTML = '#' + response.orderCode;
            cell2.innerHTML = response.riderName;
            cell3.innerHTML = response.ridestatus;
            cell4.innerHTML = response.riderContact;
          

          


        },
        error: function (error) {
            console.log(error);
        }
    })
}
function OrderCompleted(id) {
    var getrow = document.getElementById(id);
    getrow.cells[8].innerHTML = "Completed";
    getrow.setAttribute("style", "background-color:blue;color:white;");
    var rowid = "rider" + id;
    var getrow = document.getElementById(rowid);
    getrow.cells[2].innerHTML = "Completed";
    getrow.setAttribute("style", "background-color:blue;color:white;");

}
function RiderOrderAccepted(id) {
    var rowid = "rider" + id;
    var getrow = document.getElementById(rowid);
    getrow.cells[2].innerHTML = "OrderAccepted";
    getrow.setAttribute("style", "background-color:Green;color:white;");

}
function OrderPicked(id) {
    var getrow = document.getElementById(id);
    getrow.cells[8].innerHTML = "OnWay";
    getrow.setAttribute("style", "background-color:grey;color:black;");
    var rowid = "rider" + id;
    var getrow = document.getElementById(rowid);
    getrow.cells[2].innerHTML = "OnWay";
    getrow.setAttribute("style", "background-color:grey;color:black;");
}


connection.start().catch(function(err) {
    return console.error(err.toString());
});


