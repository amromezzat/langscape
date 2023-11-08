import { Card, Grid, Label, List } from "semantic-ui-react";
import { FlashCardSet } from "../../../../models/flashCards/flashCardSet";
import { FavoriteButtonComponent } from "../../../common/buttons/FavoriteButtonComponent";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../../stores/core/store";
import { UserInfoComponent } from "../../../common/buttons/SetCreatorInfoComponent";
import '../../../../styles/Common.css';
import '../../../../styles/CardSetComponent.css';
import { router } from "../../../../routes/Routes";
import { SyntheticEvent } from "react";
import { User } from "../../../../models/user/user";
import { Link, useParams } from "react-router-dom";

interface Props {
    cardSet: FlashCardSet
}

export default observer(function CardSetComponent({cardSet}: Props) {
    const { username } = useParams();
    const { flashCardStore } = useStore();
    const { submittingFavoriteSetsId: submittingFavoriteSetId, addFavoriteSet, removeFavoriteSet } = flashCardStore;

    function handleUserInfoClick(event: SyntheticEvent, user: User) {
        event.preventDefault();
        if(user === undefined) return;
        
        flashCardStore.setFilter('userId', user.id);
        router.navigate(`/${user.username}/sets`);
    }

    function handleFavoriteClick(event: SyntheticEvent) {
        event.preventDefault();
        cardSet.isFavorite ? removeFavoriteSet(cardSet.id) : addFavoriteSet(cardSet.id);
    }
    
    return (
        <Card 
            className='card set'
            as={Link} to={`/sets/${cardSet.id}`}
        >
            <Grid>
                <Grid.Row columns='equal'>
                    <Grid.Column >
                        { !username ? <UserInfoComponent
                            buttonProps={ {basic: true, floated: 'left', className: 'borderless-button'} } 
                            setMeta={ cardSet.meta }
                            onClick={ handleUserInfoClick }
                        /> : null 
                        }
                    </Grid.Column>
                    <Grid.Column >
                        <FavoriteButtonComponent 
                            isFavorite={ cardSet.isFavorite }
                            isSubmitting={ submittingFavoriteSetId.has(cardSet.id) }
                            fieldProps={ {basic: true, floated: 'right', className: 'borderless-button'} }
                            onClick={ handleFavoriteClick }
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
                        { cardSet.previewWords?.length }&nbsp;&nbsp;words
                    </Label>
                </Card.Meta>
            </Card.Content>
            <Card.Content>
                <Card.Description>
                    <List>
                        {cardSet.previewWords?.map(word => (
                            <List.Item key={ word.id }>{ word.word } | {word.translation }</List.Item>
                        ))}
                    </List>
                </Card.Description>
            </Card.Content>
        </Card>
    )
});