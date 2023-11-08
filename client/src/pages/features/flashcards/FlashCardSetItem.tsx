import { observer } from "mobx-react-lite";
import CardSetHeaderComponent from "../../../components/features/flashCards/setView/CardSetHeaderComponent";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useStore } from "../../../stores/core/store";
import { FlashCardSet } from "../../../models/flashCards/flashCardSet";
import { Loader } from "semantic-ui-react";
import CardSetExpandedComponent, { MovementType } from "../../../components/features/flashCards/setView/SetFlipCardComponent";
import FlashCardSetPagination from "../../../components/features/flashCards/setView/FlashCardSetPagination";
import { sleep } from "../../../services/api/core/utility";

export default observer (function FlashCardSetItem() {
    const swipeAnimationTime = 0.1;
    const {flashCardStore} = useStore();
    const {loadSet, loading, setLoading} = flashCardStore;
    const {id} = useParams();
    const [set, setSet] = useState<FlashCardSet | undefined>(undefined);
    const [index, setIndex] = useState<number>(1);
    const [isFlipped, setIsFlipped] = useState<boolean>(false);
    const [movementType, setMovementType] = useState<MovementType>(MovementType.None);

    function handleLeftClicked() {
        if(index === 1) return;
        setIsFlipped(false);
        setMovementType(MovementType.Left);
        sleep(swipeAnimationTime).then(() => {
            setIndex(index - 1);
            setMovementType(MovementType.None);
        });
    }

    function handleRightClicked() {
        if(index === set!.words.length) return;
        setIsFlipped(false);
        setMovementType(MovementType.Right);
        sleep(swipeAnimationTime).then(() => {
            setIndex(index + 1);
            setMovementType(MovementType.None);
        });
    }

    function handleFlip() {
        setIsFlipped(!isFlipped);
    }

    function handleMoveEnd() {
        setMovementType(MovementType.None);
    }

    useEffect(() => {
        if(id && set === undefined) {
            loadSet(id!).then(set => setSet(set));
        }
        return () => {
            setLoading(false);
        }
    }, [id, loadSet, setLoading, setSet, set])

    return (
        <>
            {loading || set === undefined ? (
                <Loader active inline/>
            ) : (
                <>
                    <CardSetHeaderComponent setName={set.name} />
                    <CardSetExpandedComponent 
                        word={set.words[index - 1]}
                        isFlipped={isFlipped}
                        movementType={movementType}
                        handleClick={handleFlip}
                        handleMoveEnd={handleMoveEnd}
                    />
                    <FlashCardSetPagination 
                        index={index} 
                        wordsCount={set.words.length}
                        handleLeftClicked={handleLeftClicked}
                        handleRightClicked={handleRightClicked} 
                    />
                </>
            )}
        </>
    )
})