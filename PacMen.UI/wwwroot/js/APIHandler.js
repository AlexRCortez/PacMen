//var uri = "https://bigprojectapi-300054925.azurewebsites.net/api/score";
//var uriU = "https://bigprojectapi-300054925.azurewebsites.net/api/user";
//var scoreId = "780abd4b-c783-4920-8cc8-3203a59af25e";
//var outputElement = document.getElementById('output');
//var outputUserElement = document.getElementById('outputuser');
//var addScore = document.getElementById('gameOverScoreLabel').innerHTML.value;
//var addLevel = document.getElementById('gameOverScoreLabel2').innerHTML.value;
//var date = Date.now();

//function getItems() {
//    fetch(uri)
//        .then(response => response.json())
//        .then(data => {
//                outputElement.textContent = JSON.stringify(data, null, 2);
//            })
//        .catch(error => console.error('Unable to get items.', error));
 
//}


////fetch(uri)
////    .then(response => {
////        if (!response.ok) {
////            if (response.status === 404) {
////                throw new Error('Data not found');
////            } else if (response.status === 500) {
////                throw new Error('Server error');
////            } else {
////                throw new Error('Network response was not ok');
////            }
////        }
////        return response.json();
////    })
////    .then(data => {
////        outputElement.textContent = JSON.stringify(data, null, 2);
////    })
////    .catch(error => {
////        console.error('Error:', error);
////    });



////async function getUserItems() {

////    const response = await fetch(uriU)
////        .then(response => {
////            if (!response.ok) {
////                if (response.status === 404) {
////                    throw new Error('Data not found');
////                } else if (response.status === 500) {
////                    throw new Error('Server error');
////                } else {
////                    throw new Error('Network response was not ok');
////                }
////            }
////            return response.json();
////        })
////        .then(data => {
////            outputUserElement.textContent = JSON.stringify(data, null, 2);
////        })
////        .catch(error => {
////            console.error('Error:', error);
////        });
////}
////getUserItems();
//getItems();

//function updateItem() {

//    const item = {
//        id: scoreId,
//        scores: 3,
//        date: date,
//        level: 34
//    };

//    fetch(uri + "/" + scoreId, {
//        method: 'PUT',
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json; charset=UTF-8'
//        },
//        body: JSON.stringify(item)
//    })
//        .then(() => getItems())
//        .catch(error => console.error('Unable to update item.', error));
//}

//updateItem();

////function updateUserItem() {

////    const item = {
////        id: 20,
////        scores: 10,
////        date: date,
////        level: 1000

////        //id: scoreId,
////        //scores: document.getElementById('gameOverScoreLabel').value.trim(),
////        //date: date,
////        //level: document.getElementById('gameOverScoreLabel2').value.trim(),
////    };

////    fetch(`${uriU}`, {
////        method: 'PUT',
////        headers: {
////            'Accept': 'application/json',
////            'Content-Type': 'application/json; charset=UTF-8'
////        },
////        body: JSON.stringify(item)


////    })
////        .then(() => getItems())
////        .catch(error => console.error('Unable to update item.', error));

////    return false;
////}
////updateUserItem();
////function displayScores(id){
////    var item = todos.find(item => item.id === id);
    

////    scoreId = item.id;
////    document.getElementById('gameOverScoreLabel').value = item.scores;
////    document.GetElementById('gameOverScoreLavel2').value = item.level;
////    document.getElementById('edit-date').value = item.date;


////}

////function updateItem() {
////    const itemId = scoreId;
    
////    const item = {
////        id: scoreId,
////        scores: addScore,
////        level: addLevel,
////        Date: date

////    }
////    fetch(`${uri}/${scores}`, {
////        method: 'PUT',
////        headers: {
////            'Accept': 'application/json',
////            'Content-Type': 'application/json'
////        },
////        body: JSON.stringify(item)
////    })
////        .then(() => getItems())
////        .catch(error => console.error('Unable to update item.', error));

  

////    return false;
////}






















////var scoreTs = document.querySelector('#gameOverScoreLabel')
////var LevelTs = document.querySelector('#gameOverScoreLabel2')


////function* getDataCoroutine() {
////    const uri = "https://bigprojectapi-300054925.azurewebsites.net/api/user/";
////    const request = yield fetch(uri);
////    const responseData = yield request.text();
////    const users = parseJsonArray(responseData);

////    let loginSuccessful = false;
////    for (let user of users) {
////        if (user.userName === username) {
////            const hashedPassword = hashPassword(password);

////            if (user.password === hashedPassword) {
////                loginSuccessful = true;
////                scoreId = user.scoreId;
////                break;
////            }
////        }
////    }
////}



//// Technology stack: Unity
////function* updateScoreCoroutine(scoreId, scoreTs, LevelTs ) {
////    const updatedScoreData = {
////        id: scoreId,
////        scores: scoreTs,
////        date: new Date(),
////        level: LevelTs
////    };
////    const jsonRequestBody = JSON.stringify(updatedScoreData);

////    const uri = `https://bigprojectapi-300054925.azurewebsites.net/api/score/${scoreId}`;

////    const request = new XMLHttpRequest();
////    request.open("PUT", uri, true);
////    request.setRequestHeader("Content-Type", "application/json");

////    request.onload = function () {
////        if (request.status >= 200 && request.status < 400) {
////            console.log("Score updated successfully");
////        } else {
////            console.error("Error updating score: " + request.statusText);
////        }
////    };

////    request.onerror = function () {
////        console.error("Error updating score");
////    };

////    request.send(jsonRequestBody);
////}





////var apiUrl = "https://bigprojectapi-300054925.azurewebsites.net/api/score";
////var outputElement = document.getElementById('output');
////var scoresT = document.querySelector('#scores');

////var levelsEL = document.querySelector('#LevelEL');


////fetch(apiUrl)
////    .then(response => {
////        if (!response.ok) {
////            if (response.status === 404) {
////                throw new Error('Data not found');
////            } else if (response.status === 500) {
////                throw new Error('Server error');
////            } else {
////                throw new Error('Network response was not ok');
////            }
////        }
////        return response.json();
////    })
////    .then(data => {
////        outputElement.textContent = JSON.stringify(data, null, 2);
////    })
////    .catch(error => {
////        console.error('Error:', error);
////    });

////// Data to be sent in the POST request (in JSON format)
////var data = {
////    level: 1, 
////    score: 900,
////    date: "2015-03-25"
////};

////var requestOptions = {
////    method: 'PUT',
////    headers: { 'Content-Type': 'application/json' },
////    body: JSON.stringify({ title: 'Fetch PUT Request Example' })
////};
////fetch(apiUrl, requestOptions)
////    .then(async response => {
////        const isJson = response.headers.get('content-type')?.includes('application/json');
////        const data = isJson && await response.json();

////        // check for error response
////        if (!response.ok) {
////            // get error message from body or default to response status
////            const error = (data && data.message) || response.status;
////            return Promise.reject(error);
////        }
////        console.log('Post request response:', data);
////        /*element.innerHTML = data.updatedAt;*/
////    })
////    .catch(error => {
////       /* element.parentElement.innerHTML = `Error: ${error}`;*/
////        console.error('There was an error!', error);
////    });

///*const data = { username: "example" };*/
///*postJSON(data);*/




////// POST request options
////var requestOptions = {
////    method: 'POST',
////    headers: {'Content-Type': 'application/json'},
////    body: JSON.stringify(postData)
////};

////async function DataPost(apiUrlScore, postData = {}) {
////    const response = await fetch(apiUrlScore, {
////        method: "POST",
////        headers: {
////            "Content-Type": "application/json"
////        },
////        body: JSON.stringify(data)
////    });

////    return await response.json();
////}

//// Make the POST request
////fetch(apiUrl, requestOptions)
////    .then(response => {
////        // Check if the request was successful
////        if (!response.ok) {
////            throw new Error('Network response was not ok');
////        }
////        // Parse the JSON response
////        return response.json();
////    })
////    .then(postData => {
////        // Handle the data returned from the server
////        console.log('Post request response:', postData);
////    })
////    .catch(error => {
////        // Handle any errors that occurred during the fetch
////        console.error('There was a problem with the fetch operation:', error);
////    });