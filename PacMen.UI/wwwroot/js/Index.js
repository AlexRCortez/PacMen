// Write your JavaScript code.
const canvas = document.querySelector('canvas');
const c = canvas.getContext('2d');
const scoreT = document.querySelector('#scores');

const levelEL = document.querySelector('#LevelEL');
//console.log(scores);
//canvas.width = Boundary.width * 11;
//canvas.height = Boundary.height * 13;
const maps = [
  
    [
        ['c1', '-', '-', '-', '-', '-', '-', '-', '-', '-', 'c2'],
        ['|', '', '.', '.', '.', '.', '.', '.', '.', 'I', '|'],
        ['|', '.', 'b', '.', '[', '7', ']', '.', 'b', '.', '|'],
        ['|', '.', '.', '.', '.', '|', '.', '.', '.', '.', '|'],
        ['|', '.', '[', ']', '.', '_', '.', '[', ']', '.', '|'],
        ['_', '.', '.', '.', '.', '.', '.', '.', '.', '.', '_'],
        ['.', '.', 'b', '.', '[', '5', ']', '.', 'b', '.', '.'],
        ['^', '.', '.', '.', '.', '.', '.', '.', '.', '.', '^'],
        ['|', '.', '[', ']', '.', '^', '.', '[', ']', '.', '|'],
        ['|', '.', '.', '.', '.', '|', '.', '.', '.', '.', '|'],
        ['|', '.', 'b', '.', '[', '5', ']', '.', 'b', '.', '|'],
        ['|', 'I', '.', '.', '.', '.', '.', '.', '.', 'p', '|'],
        ['c4', '-', '-', '-', '-', '-', '-', '-', '-', '-', 'c3'],
    ],
   
    [
        ['c1', '-', '-', '-', ']', '.', '[', '-', '-', '-', 'c2'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', 'I', '|'],
        ['|', '.', '[', ']', '.', 'b', '.', '[', ']', '.', '|'],
        ['_', '.', '.', '.', '.', '.', '.', '.', '.', '.', '_'],
        ['.', '.', 'b', '.', '[', '5', ']', '.', 'b', '.', '.'],
        ['^', '.', '.', '.', '.', '.', '.', '.', '.', '.', '^'],
        ['|', '.', '[', ']', '.', 'b', '.', '[', ']', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['|', '.', '[', ']', '.', 'I', '.', 'b', '.', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['c4', '-', '-', '-', ']', '.', '[', '-', '-', '-', 'c3'],
    ],
    [
        ['c1', '-', '-', '-', '-', '-', '-', '-', '-', '-', 'c2'],
        ['|', '', '.', '.', '.', '.', '.', '.', '.', 'I', '|'],
        ['|', '.', 'b', '.', '[', '7', ']', '.', 'b', '.', '|'],
        ['|', '.', '.', '.', '.', '|', '.', '.', '.', '.', '|'],
        ['|', '.', '[', ']', '.', '_', '.', '[', ']', '.', '|'],
        ['_', '.', '.', '.', '.', '.', '.', '.', '.', '.', '_'],
        ['.', '.', 'b', 'p', '[', '5', ']', '.', 'b', '.', '.'],
        ['^', '.', '.', '.', '.', '.', '.', '.', '.', '.', '^'],
        ['|', '.', '[', ']', '.', '^', '.', '[', ']', '.', '|'],
        ['|', '.', '.', '.', '.', '|', '.', '.', '.', '.', '|'],
        ['|', '.', 'b', '.', '[', '5', ']', '.', 'b', '.', '|'],
        ['|', 'I', '.', '.', '.', '.', '.', '.', '.', 'p', '|'],
        ['c4', '-', '-', '-', ']', '-', '[', '-', '-', '-', 'c3'],
    ],
    [
        ['c1', '-', '-', '-', ']', '.', '[', '-', '-', '-', 'c2'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', 'I', '|'],
        ['|', '.', '[', ']', '.', 'b', '.', '[', ']', '.', '|'],
        ['_', '.', '.', '.', '.', '.', '.', '.', '.', '.', '_'],
        ['.', '.', 'b', 'p', '[', '5', ']', '.', 'b', '.', '.'],
        ['^', '.', '.', '.', '.', '.', '.', '.', '.', '.', '^'],
        ['|', '.', '[', ']', '.', 'b', '.', '[', ']', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['|', '.', '[', ']', '.', 'I', '.', 'b', '.', '.', '|'],
        ['|', '.', '.', '.', '.', '.', '.', '.', '.', '.', '|'],
        ['c4', '-', '-', '-', ']', '.', '[', '-', '-', '-', 'c3'],
    ],

]


let pellets = []
let powerUps = []
let levelel = []
let ghosts = []
let player = {}
let items = []
let ghostSpeed = 75
const ghostSpeedI = 25;

const keys = {
    w: {
        pressed: false
    },
    a: {
        pressed: false
    },
    s: {
        pressed: false
    },
    d: {
        pressed: false
    }
}
let lastKey = '';
let score = 0;
let animationId;
let prevMs = Date.now();
let accumulatedTime = 0;
let ghostReleaseIntervals = [0, 2, 4, 6]
let currentLevelIndex = 0
let boundaries = generateBoundaries(currentLevelIndex, maps)
let lives = 3

const ghostPositions = [
   
   
    [
        {
            x: Boundary.width * 5 + Boundary.width / 2,
            y: Boundary.height * 5 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 5 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 4 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 6 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
    ],
    [
        {
            x: Boundary.width * 6 + Boundary.width / 2,
            y: Boundary.height * 4 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 5 + Boundary.width / 2,
            y: Boundary.height * 5 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 4 + Boundary.width / 2,
            y: Boundary.height * 5 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 6 + Boundary.width / 2,
            y: Boundary.height * 5 + Boundary.height / 2,
        },
    ],
    [
        {
            x: Boundary.width * 5 + Boundary.width / 2,
            y: Boundary.height * 5 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 5 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 4 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
        {
            x: Boundary.width * 6 + Boundary.width / 2,
            y: Boundary.height * 6 + Boundary.height / 2,
        },
    ],
     [
     {
         x: Boundary.width * 5 + Boundary.width / 2,
         y: Boundary.height * 5 + Boundary.height / 2,
     },
     {
         x: Boundary.width * 5 + Boundary.width / 2,
         y: Boundary.height * 6 + Boundary.height / 2,
     },
     {
         x: Boundary.width * 4 + Boundary.width / 2,
         y: Boundary.height * 6 + Boundary.height / 2,
     },
     {
         x: Boundary.width * 6 + Boundary.width / 2,
         y: Boundary.height * 6 + Boundary.height / 2,
     },
 ],


]



const game = {
    isPaused: false,
    init() {
        pellets = []
        powerUps = []
        items = []
        accumulatedTime = 0
        player = new Player({
            position: {
                x: Boundary.width + Boundary.width / 2,
                y: Boundary.height + Boundary.height / 2,
            },
            velocity: {
                x: 0,
                y: 0,
            },
        }),
        ghosts = [
            new Ghost({
                position: ghostPositions[currentLevelIndex][0],
                velocity: {
                    x: Ghost.speed * (Math.random() < 0.5) ? -1 : 1,
                    y: 0,
                },
                imgSrc: './img/sprites/orangeGhost.png',
                state: 'active',
                speed: ghostSpeed,
                outofCage: true,
            }),
            new Ghost({
                position: ghostPositions[currentLevelIndex][1],
                velocity: {
                    x: Ghost.speed * (Math.random() < 0.5) ? -1 : 1,
                    y: 0,
                },
                imgSrc: './img/sprites/greenGhost.png',
                state: 'active',
                speed: ghostSpeed,
            }),
            new Ghost({
                position: ghostPositions[currentLevelIndex][2],
                velocity: {
                    x: Ghost.speed * (Math.random() < 0.5) ? -1 : 1,
                    y: 0,
                },
                imgSrc: './img/sprites/redGhost.png',
                speed: ghostSpeed,
            }),
            new Ghost({
                position: ghostPositions[currentLevelIndex][3],
                velocity: {
                    x: Ghost.speed * (Math.random() < 0.5) ? -1 : 1,
                    y: 0,
                },
                imgSrc: './img/sprites/yellowGhost.png',
                speed: ghostSpeed,
            }),
            ]

        boundaries = generateBoundaries(currentLevelIndex, maps)
    },
    initStart() {
        
        player.state = 'paused'
        ghosts.forEach((ghost) => {
            ghost.state = 'paused'
        })

        setTimeout(() => {
            ghosts[0].state = 'active'
            ghosts[1].state = null
            ghosts[2].state = null
            ghosts[3].state = null
            player.state = 'active'
        }, 1000)
    },
    nextRound() {
        scoreT.innerHTML = score
        levelEL.innerHTML = currentLevelIndex +2;
        player.state = 'paused'
        ghosts.forEach((ghost) => {
            ghost.state = 'paused'
        })

        ghostSpeed += ghostSpeedI
        ghostReleaseIntervals = ghostReleaseIntervals.map((interval, index) => {
            if (index === 0) return interval
            else if (index === 1 && interval > 1) return interval - 1
            else if (index === 2 && interval > 2) return interval - 1
            else if (index === 3 && interval > 3) return interval - 1
        })

        setTimeout(() => {
            currentLevelIndex++
            if (currentLevelIndex > maps.length - 1) currentLevelIndex = 0
            boundaries = generateBoundaries(currentLevelIndex, maps)
            game.init()
            game.initStart()
        }, 1000)
    
    },
    end() {
        document.querySelector('#gameOverScoreLabel').innerHTML = score
        document.querySelector('#gameOverScoreLabel2').innerHTML = currentLevelIndex + 1;
        document.querySelector('#gameOverScreen').style.display = 'block'
    },
    restart() {
        currentLevelIndex = 0
        boundaries = generateBoundaries(currentLevelIndex, maps)
        lives = 3
        score = 0
        scoreT.innerHTML = score
        levelEL.innerHTML = currentLevelIndex + 1;
    },


}
game.init();


function animate() {
    animationId = requestAnimationFrame(animate);
    c.clearRect(0, 0, canvas.width, canvas.height);

    const currentMs = Date.now()
    const delta = (currentMs - prevMs) / 1000
    prevMs = currentMs

    if (player.state === 'active') accumulatedTime += delta;

    accumulatedTime += delta;

    if (keys.w.pressed && lastKey === 'w') player.move('up');
    else if (keys.a.pressed && lastKey === 'a')  player.move('left');
    else if (keys.s.pressed && lastKey === 's') player.move('down');
    else if (keys.d.pressed && lastKey === 'd') player.move('right');

    // detect collisions between ghost and player

    for (let i = ghosts.length - 1; 0 <= i; i--) {
        const ghost = ghosts[i];
        if (Math.hypot(ghost.position.x - player.position.x,
            ghost.position.y - player.position.y) < ghost.radius + player.radius && player.state === 'active')
        {
            if (ghost.state === 'scared') {
                ghosts.splice(i, 1)
            } else {
                lives--
                player.die(lives, game)
                scoreT.innerHTML = 0;
                ghosts.forEach((ghost) => {
                    ghost.state = 'paused'
                })
                //cancelAnimationFrame(animationId);
                console.log('you loose');
            }
        }
    }
    // win condition goes here
    if (pellets.length === 10 && player.state === 'active') {
        game.nextRound()
    }

    //pwer ups

    // power ups go
    for (let i = powerUps.length - 1; 0 <= i; i--) {
        const powerUp = powerUps[i]
        powerUp.draw()

        // player collides with powerup
        if (
            Math.hypot(
                powerUp.position.x - player.position.x,
                powerUp.position.y - player.position.y,
            ) <
            powerUp.radius + player.radius
        ) {
            powerUps.splice(i, 1)

            // make ghosts scared
            ghosts.forEach((ghost) => {
                ghost.state = 'preScared'

                setTimeout(() => {
                    ghost.state = 'preActive'
                }, 5000)
            })
        }
    }
    

    // for our items
    for (let i = items.length - 1; 0 <= i; i--) {
        const item = items[i]
        item.draw()

        // player collides with item
        if (
            Math.hypot(
                item.position.x - player.position.x,
                item.position.y - player.position.y,
            ) <
            item.radius + player.radius
        ) {
            items.splice(i, 1)
            score += 50
            scoreT.innerHTML = score;
        }
    }


    //Pallet and player collide 
    for (let i = pellets.length - 1; 0 <= i; i--) {
        const pellet = pellets[i]; //iterate backwords so falshing does not happen

        pellet.draw();

        if (
            Math.hypot(pellet.position.x - player.position.x, pellet.position.y - player.position.y) < pellet.radius + player.radius) {
            // console.log('touching')
            pellets.splice(i, 1);
            score += 10;
            scoreT.innerHTML = score;
        }

    }

    boundaries.forEach((boundary) => {
        boundary.draw()
    })
    player.update(delta, boundaries);


    //Ghost Loop
    ghosts.forEach((ghost, index) => {
        ghost.update(delta, boundaries)


        if (accumulatedTime > ghostReleaseIntervals[index] && !ghost.outofCage)
            ghost.enterGame(ghostPositions[currentLevelIndex][1])
        //console.log(collisions);
    })

    if (player.velocity.x > 0) player.rotation = 0
    else if (player.velocity.x < 0) player.rotation = Math.PI
    else if (player.velocity.y > 0) player.rotation = Math.PI /2
    else if (player.velocity.y < 0) player.rotation = Math.PI *1.5
}
animate();




document.querySelector('#restartGameButton').addEventListener('click', (e) => {
    document.querySelector('#gameOverScreen').style.display = 'none'
    document.querySelector('#readyTag').style.display = 'block'
    setTimeout(() => {
        game.restart()
        game.init()
        document.querySelector('#readyTag').style.display = 'none'
        document.querySelector('#goTag').style.display = 'block'
        gsap.to('#goTag', {
            delay: 0.5,
            opacity: 0,
        })
    }, 2000)
    
})

document.querySelector('#startButton').addEventListener('click', (e) => {
    document.querySelector('#startScreen').style.display = 'none'
    document.querySelector('#readyTag').style.display = 'block'
    setTimeout(() => {
        game.init()
        document.querySelector('#readyTag').style.display = 'none'
        document.querySelector('#goTag').style.display = 'block'
        gsap.to('#goTag', {
            delay: 0.5,
            opacity: 0,
        })
    }, 2000)
})
//console.log("end");
