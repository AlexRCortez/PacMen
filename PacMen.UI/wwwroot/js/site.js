//var uri = "https://bigprojectapi-300054925.azurewebsites.net/api/score";
//var uriU = "https://bigprojectapi-300054925.azurewebsites.net/api/user";


//fetch(uriU)
//    .then(response => {
//        const data = response.users;
//        let rows = '';
//        data.foreach(users => {
//            rows += `<tr><td>${users.username}</td></tr>`
//        })
//    })
//    .catch(error => console.error('Unable to get items.', error));


fetch("https://bigprojectapi-300054925.azurewebsites.net/api/score")
    .then(res => res.json())
    .then(data => {
        console.log(data.data[2].date)
    })
    .catch (error => {
    console.error(error)
});


//fetch("https://bigprojectapi-300054925.azurewebsites.net/api/score")
//    .then(response => response.json())
//    .then(res => {
//        const data = res.score;
//        let rows = '';
//        data.forEach(score => {
//            rows += `<tr><td>${score.scores}</td><td>${score.level}</td><td>${score.date}</td></tr>`
//        })
//        document.getElementById('tableRows').innerHTML = rows;
//    })
//    .catch(error => console.error(error));

