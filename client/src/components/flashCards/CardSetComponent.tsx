import { Card, Label, List } from "semantic-ui-react";
import { FlashCardSet } from "../../models/flashCards/flashCardSet";
import { FavoriteButtonComponent } from "../common/FavoriteButtonComponent";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/core/store";
import '../../styles/Common.css';
import '../../styles/CardSetComponent.css';

interface Props {
    cardSet: FlashCardSet
}

export default observer(function CardSetComponent({cardSet}: Props) {
    const { flashCardStore } = useStore();
    const { submittingFavoriteSetsId: submittingFavoriteSetId, addFavoriteSet, removeFavoriteSet } = flashCardStore;
    
    return (
        <Card 
            className='card set'
            href='#card-example-link-card'
        >
            <FavoriteButtonComponent 
                isFavorite={cardSet.isFavorite}
                isSubmitting={ submittingFavoriteSetId.has(cardSet.id) }
                fieldProps={{basic: true, attached: 'left', className: 'borderless-button'}}
                onClick={ () => cardSet.isFavorite ? removeFavoriteSet(cardSet.id) : addFavoriteSet(cardSet.id) }
            />
            <Card.Content>
                <Card.Header>
                    {cardSet.name}
                </Card.Header>
                <Card.Meta>
                    <Label color='blue' circular size="small">
                        {cardSet.words.length}&nbsp;&nbsp;words
                    </Label>
                </Card.Meta>
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
});