function successMessage(message) {
  Swal.fire({
    title: "Success",
    text: message,
    icon: "success",
  });
}

function confirmMessage(message) {
  // return new Promise((resolve, reject) => {
  //   Swal.fire({
  //     title: "Confirm",
  //     text: message,
  //     icon: "warning",
  //     showCancelButton: true,
  //     confirmButtonText: "Yes",
  //   }).then((result) => {
  //      resolve(result.isConfirmed);
  //   });
  // });

  return new Promise((resolve, reject) => {
    Notiflix.Confirm.show(
      'Confirm',
      message,
      'Yes',
      'No',
      () => {
        resolve(true);
      },
      () => {
        resolve(false);
      },
      );
  });

    // return new Promise((resolve, reject) => {
    //     const result = confirm(message);
    //     resolve(result);
    // })
}
