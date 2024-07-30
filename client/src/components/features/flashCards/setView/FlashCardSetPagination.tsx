import { Button, ButtonProps, Segment } from "semantic-ui-react";
import "../../../../styles/Common.css"

interface Props {
    index: number;
    wordsCount: number;
    handleLeftClicked: () => void;
    handleRightClicked: () => void;
}

export default function FlashCardSetPagination({index, wordsCount, handleLeftClicked, handleRightClicked}: Props) {
    let buttonProps: ButtonProps = {
        basic: true, 
        inverted: true, 
        circular: true, 
        fitted: 'true', 
        size: 'large'
    };
    
    return (
        <Segment basic style={{maxWidth: '75%'}} className='white-text data'>
            <Button 
                {...buttonProps}
                icon='angle left'
                onClick={handleLeftClicked}
                disabled={index === 1}
            />
            <strong>{`      ${index}    /   ${wordsCount}      `}</strong>
            <Button 
                {...buttonProps}
                icon='angle right'
                onClick={handleRightClicked}
                disabled={index === wordsCount}
            />
        </Segment>
    )
}