import { Card, Grid, Label, List } from "semantic-ui-react";
import { FlashCardSet } from "../../../models/flashCards/flashCardSet";
import { FavoriteButtonComponent } from "../../common/buttons/FavoriteButtonComponent";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/core/store";
import { UserInfoComponent } from "../../common/buttons/SetCreatorInfoComponent";
import '../../../styles/Common.css';
import '../../../styles/CardSetComponent.css';

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
            <Grid>
                <Grid.Row columns='equal'>
                    <Grid.Column >
                        <UserInfoComponent
                            buttonProps={ {basic: true, floated: 'left', className: 'borderless-button'} } 
                            setMeta={ cardSet.meta }
                        />
                    </Grid.Column>
                    <Grid.Column >
                        <FavoriteButtonComponent 
                            isFavorite={ cardSet.isFavorite }
                            isSubmitting={ submittingFavoriteSetId.has(cardSet.id) }
                            fieldProps={ {basic: true, floated: 'right', className: 'borderless-button'} }
                            onClick={ () => cardSet.isFavorite ? removeFavoriteSet(cardSet.id) : addFavoriteSet(cardSet.id) }
                        />
                    </Grid.Column>
                </Grid.Row>
            </Grid>
            <Card.Content textAlign='center'>
                <Card.Header>
                    { cardSet.name }
                </Card.Header>
                <Card.Meta>
                    <Label color='blue' circular size='small'>
                        { cardSet.words.length }&nbsp;&nbsp;words
                    </Label>
                </Card.Meta>
            </Card.Content>
            <Card.Content>
                <Card.Description>
                    <List>
                        {cardSet.words.map(word => (
                            <List.Item key={ word.id }>{ word.word } | {word.translation }</List.Item>
                        ))}
                    </List>
                </Card.Description>
            </Card.Content>
        </Card>
    )
});