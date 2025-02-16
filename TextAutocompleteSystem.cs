import java.util.*;

class AutocompleteSystem {
    private TrieNode root;
    private StringBuilder currentInput;

    public AutocompleteSystem() {
        root = new TrieNode();
        currentInput = new StringBuilder();
    }

    public void insert(String word) {
        TrieNode node = root;
        for (char c : word.toCharArray()) {
            node.children.putIfAbsent(c, new TrieNode());
            node = node.children.get(c);
        }
        node.isEndOfWord = true;
    }

    public List<String> search(String prefix) {
        TrieNode node = root;
        for (char c : prefix.toCharArray()) {
            if (!node.children.containsKey(c)) {
                return new ArrayList<>();
            }
            node = node.children.get(c);
        }
        List<String> results = new ArrayList<>();
        dfs(node, new StringBuilder(prefix), results);
        return results;
    }

    private void dfs(TrieNode node, StringBuilder prefix, List<String> results) {
        if (node.isEndOfWord) {
            results.add(prefix.toString());
        }
        for (Map.Entry<Character, TrieNode> entry : node.children.entrySet()) {
            prefix.append(entry.getKey());
            dfs(entry.getValue(), prefix, results);
            prefix.deleteCharAt(prefix.length() - 1);
        }
    }

    static class TrieNode {
        Map<Character, TrieNode> children = new HashMap<>();
        boolean isEndOfWord = false;
    }

    public static void main(String[] args) {
        AutocompleteSystem autocompleteSystem = new AutocompleteSystem();
        autocompleteSystem.insert("apple");
        autocompleteSystem.insert("app");
        autocompleteSystem.insert("application");
        autocompleteSystem.insert("applet");

        System.out.println(autocompleteSystem.search("app")); // [app, apple, application, applet]
        System.out.println(autocompleteSystem.search("apple")); // [apple, applet]
        System.out.println(autocompleteSystem.search("applz")); // []
    }
}
