import { makeAutoObservable, reaction, runInAction } from "mobx";
import { FlashCardSet } from "../../models/flashCards/flashCardSet";
import { flashCardApi } from "../../services/api/features/flashcards/flashCardApi";
import { FlashCardWord } from "../../models/flashCards/flashCardWord";
import { FlashCardSetDto } from "../../models/flashCards/flashCardSetDto";
import { FlashCardSetForm } from "../../models/flashCards/flashCardSetForm";

export default class FlashCardStore {
    setsRegistery = new Map<string, FlashCardSet>();
    isLoading = false;
    submittingFavoriteSetsId = new Set<string>();
    filters = new Map<string, any>();

    constructor() {
        makeAutoObservable(this);

        reaction (
            () => this.filters.keys(),
            () => {
                this.setsRegistery.clear();
                this.loadSets();
            }
        )
    }

    setIsLoading = (state: boolean) => this.isLoading = state;

    loadSets = async () => {
        this.setIsLoading(true);
        try {
            const filters = this.filters.size > 0 ? this.filters : undefined;
            this.updateSetsRegistery(await flashCardApi.getCardSets(filters));
        } catch (error) {
            console.log(error);
        } 
        runInAction(() => this.setIsLoading(false));
    }

    loadSet = async (setId: string) => {
        this.setIsLoading(true);
        try {
            let set = await flashCardApi.getCardSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), ...set} as FlashCardSet);
            runInAction(() => this.setIsLoading(false));
            return set;
        } catch (error) {
            console.log(error);
            runInAction(() => this.setIsLoading(false));
        }
    }

    addFavoriteSet = async (setId: string) => {
        this.submittingFavoriteSetsId.add(setId);
        try {
            await flashCardApi.addFavoriteSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), isFavorite: true} as FlashCardSet);

        } catch (error) {
            console.log(error);
        }
        runInAction(() => this.submittingFavoriteSetsId.delete(setId));
    }

    removeFavoriteSet = async (setId: string) => {
        this.submittingFavoriteSetsId.add(setId);
        try {
            await flashCardApi.removeFavoriteSet(setId);
            this.updateSetRegistery({...this.setsRegistery.get(setId), isFavorite: false} as FlashCardSet);
        } catch (error) {
            console.log(error);
        }
        runInAction(() => this.submittingFavoriteSetsId.delete(setId));
    }

    setFilter = (key: string, value?: string) => {
        this.clearFilter();
        this.filters.set(key, value ?? true);
    }

    clearFilter = () => {
        this.filters.clear();
    }

    updateWord = async (setId: string, word: FlashCardWord) => {
        try {
            await flashCardApi.updateWord(setId, word);
            this.updateSetWord(setId, word);

        } catch (error) {
            console.log(error);
        }
    }

    updateSet = async (setDto: FlashCardSetDto) => {
        try {
            await flashCardApi.updateSet(setDto);
        } catch (error) {
            console.log(error);
        }
    }

    createSet = async (set: FlashCardSetForm) => {
        try {
            var createdSet = await flashCardApi.createSet(set);
            this.updateSetRegistery(createdSet);
        } catch (error) {
            console.log(error);
        }
    }

    deleteSet = async (setId: string) => {
        try {
            await flashCardApi.deleteSet(setId);
            runInAction(() => this.setsRegistery.delete(setId));
        } catch (error) {
            console.log(error);
        }
    }

    private updateSetsRegistery = (newSets: FlashCardSet[]) => {
        newSets.forEach(set => {
            set.meta.createdAt = new Date(set.meta.createdAt + 'Z');
            set.previewWords = set.words;
            set.words = [];
            this.updateSetRegistery(set)
        });
    }

    private updateSetRegistery = (newSet: FlashCardSet) => {
        this.setsRegistery.set(newSet.id, newSet);
    }

    private updateSetWord = (setId: string, word: FlashCardWord) => {
        var set = this.setsRegistery.get(setId);
        if(!set) return;
        set.words = [...set.words, word];
    }
}