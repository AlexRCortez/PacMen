
function generateBoundaries(currentLevelIndex,maps) {

const boundaries = []
const mapRows = maps[currentLevelIndex].length
const mapColumns = maps[currentLevelIndex][0].length
canvas.width = Boundary.width * mapColumns
canvas.height = Boundary.height * mapRows


 maps[currentLevelIndex].forEach((row, index) => {
    row.forEach((symbol, j) => {
        /*console.log(symbol)*/
        switch (symbol) {
            case '-':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeH.png')
                    })
                )
                break
            case '|':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeV.png')
                    })
                )
                break
            case 'c1':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCorner1.png')
                    })
                )
                break
            case 'c2':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCorner2.png')
                    })
                )
                break
            case 'c3':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCorner3.png')
                    })
                )
                break
            case 'c4':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCorner4.png')
                    })
                )
                break
            case 'b':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/block.png')
                    })
                )
                break
            case '[':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/cLeft.png')
                    })
                )
                break
            case ']':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/cRight.png')
                    })
                )
                break
            case '_':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/cBottom.png')
                    })
                )
                break
            case '^':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/cTop.png')
                    })
                )
                break
            case '+':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCross.png')
                    })
                )
                break
            case '5':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        color: 'blue',
                        image: createImage('./img/pipeCTop.png')
                    })
                )
                break
            case '6':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        color: 'blue',
                        image: createImage('./img/pipeCRight.png')
                    })
                )
                break
            case '7':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        color: 'blue',
                        image: createImage('./img/pipeCBottom.png')
                    })
                )
                break
            case '8':
                boundaries.push(
                    new Boundary({
                        position: {
                            x: Boundary.width * j,
                            y: Boundary.height * index
                        },
                        image: createImage('./img/pipeCLeft.png')
                    })
                )
                break
            case '.':
                pellets.push(
                    new Pellet({
                        position: {
                            x: j * Boundary.width + Boundary.width / 2,
                            y: index * Boundary.height + Boundary.height / 2
                        }
                    })
                )
                break

            case 'p':
                powerUps.push(
                    new PowerUp({
                        position: {
                            x: j * Boundary.width + Boundary.width / 2,
                            y: index * Boundary.height + Boundary.height / 2
                        }
                    })
                )
                break

            case 'I':
                items.push(
                    new Item({
                        position: {
                            x: j * Boundary.width + Boundary.width / 2,
                            y: index * Boundary.height + Boundary.height / 2,
                        },
                        imgSrc: './img/sprites/cherry.png',
                    }),
                )
            break

        }

    })
})
    return boundaries
}