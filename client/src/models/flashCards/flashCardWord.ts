export interface NewFlashCardWord {
    word: string;
    translation: string;
}

export interface FlashCardWord extends NewFlashCardWord {
    id: string;
}