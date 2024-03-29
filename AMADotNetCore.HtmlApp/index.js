const tblName = "Tbl_Name";

let _editId = null;

$("#btnSave").click(function () {
  if (_editId == null) {
    createData();
  } else {
    updateData();
  }
  readData();
});

function createData() {
  let lst = [];
  if (localStorage.getItem(tblName) != null) {
    lst = JSON.parse(localStorage.getItem(tblName));
    console.log({ lst });
  }
  const name = $("#txtName").val();
  const data = {
    Id: uuidv4(),
    Name: name,
  };
  lst.push(data);
  console.log({ lst });
  localStorage.setItem(tblName, JSON.stringify(lst));
  // alert("Saving succesful..");
  successMessage("Saving succesful..");

  $("#txtName").val("");
  $("#txtName").focus();
  readData();
}

function readData() {
  if (localStorage.getItem(tblName) == null) return;
  var jsonStr = localStorage.getItem(tblName);
  var lst = JSON.parse(jsonStr);
  let trRows = "";
  let count = 0;
  lst.forEach((item) => {
    console.log({ item });
    trRows += `
        <tr>
            <td>
                <button class="btn btn-danger btn-sm" onclick="deleteData('${
                  item.Id
                }')">
                <i class="fa-solid fa-trash"></i>
                </button>
                <button class="btn btn-warning btn-sm" onclick="editData('${
                  item.Id
                }')">
                <i class="fa-solid fa-pen-to-square"></i>
                </button>
            </td>
            <td>${++count}</td>
            <td>${item.Name}</td>
          </tr>
        `;
  });
  $("#tableData").html(trRows);
}

function editData(id) {
  if (localStorage.getItem(tblName) == null) return;
  var jsonStr = localStorage.getItem(tblName);
  var lst = JSON.parse(jsonStr);
  let data = lst.filter((item) => item.Id == id);
  console.log(data);
  if (data.length == 0) {
    alert("data not found")
    return;
  }

  var item = data[0];
  _editId = item.Id;
  $("#txtName").val(item.Name);
  $("#btnSave").attr("onclick", `updateData('${id}')`);
  $("#btnSave").html("Update");
}

function updateData(id) {
  let lst = [];
  if (localStorage.getItem(tblName) != null) {
    lst = JSON.parse(localStorage.getItem(tblName));
    console.log({ lst });
  }

  let index = lst.findIndex((x) => x.id == id);
  lst[index].Name = $("#txtName").val();
  const data = {
    Id: uuidv4(),
    Name: name,
  };
  console.log({ lst });
  localStorage.setItem(tblName, JSON.stringify(lst));
  alert("updating succesful..");
  readData();
}

function deleteData(id) {
  //   let result = confirm("Are you sure you want to delete this data?");
  //   if(!result) return;
  confirmMessage(
    "Are you sure you want to delete this data?").then(function(result){
      let lst = [];
      if (localStorage.getItem(tblName) == null) return;

      var jsonStr = localStorage.getItem(tblName);
      lst = JSON.parse(jsonStr);
      lst = lst.filter((item) => item.Id != id);
      localStorage.setItem(tblName, JSON.stringify(lst));

      alert("Deleting succesful..");
      readData();
    })
}

function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (c / 4)))
    ).toString(16)
  );
}
