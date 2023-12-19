import { Input } from "semantic-ui-react";

interface Props {
    initial?: string,
    setWord: (word: string) => void
}

export default function SetWordsListWordForm({ initial, setWord }: Props) {

    return ( 
        <Input 
            defaultValue = {initial}
            onChange={(event, data) => setWord(data.value)}
        />
    )
}