if (typeof(Storage) !== "undefined") {
        localStorage.setItem("username", "gingerphoenix10");
        localStorage.setItem("password", "haha no");
    } else {
        console.log("Sorry! No Web Storage support..")
    }