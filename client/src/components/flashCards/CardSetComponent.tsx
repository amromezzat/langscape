import { Card, Label, List } from "semantic-ui-react";
import { FlashCardSet } from "../../models/flashCards/flashCardSet";
import '../../styles/CardSetComponent.css';

interface Props {
    cardSet: FlashCardSet
}

const CardSetComponent = ({cardSet}: Props) => {
    return (
        <Card 
            className='card set'
            href='#card-example-link-card'
        >
            <Card.Content>
                <Card.Header>
                    {cardSet.name}
                </Card.Header>
                <Label color='blue' circular size="small">
                    {cardSet.words.length}&nbsp;&nbsp;words
                </Label>
            </Card.Content>
            <Card.Content>
                <Card.Description>
                    <List>
                        {cardSet.words.map(word => (
                            <List.Item key={word.id}>{word.word} | {word.translation}</List.Item>
                        ))}
                    </List>
                </Card.Description>
            </Card.Content>
        </Card>
    )
};

export default CardSetComponent;