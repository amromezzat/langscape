import { Header, Segment } from "semantic-ui-react"
import { FlashCardWord } from "../../../../models/flashCards/flashCardWord"
import { Link } from "react-router-dom";
import '../../../../styles/FlipCardComponent.css'

interface Props {
    word: FlashCardWord;
    isFlipped: boolean;
    movementType: MovementType;
    handleClick: () => void;
    handleMoveEnd: () => void;
}

export enum MovementType {
    None,
    Left,
    Right
}

export default function SetFlipCardComponent({ word, isFlipped, movementType, handleClick }: Props) {
    function getAnimationClassName() {
        let className = '';
        if(isFlipped) {
            className += 'card-flip-animation ';
        }
        switch(movementType) {
            case MovementType.Left:
                className += 'card-move-left-animation';
                break;
            case MovementType.Right:
                className += 'card-move-right-animation';
                break;
        }
        return className;
    }

    return (
        <Segment
            placeholder
            as={Link}
            onClick={handleClick}
            className={`flip-card ${getAnimationClassName()}`}
        >
                {isFlipped ? (
                    <Header as='h1' className='flip-card-back'>
                        {word.translation}
                    </Header>
                ) : (
                    <Header as='h1' className='flip-card-front'>
                        {word.word}
                    </Header>
                )}
        </Segment>
    )
}