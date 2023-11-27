import 'package:flutter/material.dart';

void main() {
  runApp(MaterialApp(
    home: CardList(),
  ));
}

class CardList extends StatefulWidget {
  @override
  _CardListState createState() => _CardListState();
}

class _CardListState extends State<CardList> {
  List<Widget> cards = [];

  @override
  void initState() {
    super.initState();
    for (int i = 0; i < 3; i++) {
      cards.add(buildCard("Notes za raspored $i ", "Sub title"));
    }
  }

  Color myCustomColor2 = Color(0xFFFFFFFF);
  Color myCustomColor = Color(0xFF1A237E);

  Widget buildCard(String title, String subTitle) {
    return Dismissible(
      key: UniqueKey(),
      confirmDismiss: (DismissDirection direction) async {
        if (direction == DismissDirection.startToEnd) {
          return await showDialog(
              context: context,
              builder: (BuildContext context) {
                return Dialog(
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(20.0),
                  ),
                  elevation: 0,
                  backgroundColor: Colors.transparent,
                  child: AlertDialog(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(20.0),
                    ),
                    title: Text('Delete'),
                    content: Container(
                      width: 300,
                      child: Text('Are you sure to delete?'),
                    ),
                    contentPadding: EdgeInsets.symmetric(horizontal: 24, vertical: 20),
                    actions: [
                      TextButton(
                        child: const Text(
                          "Yes",
                          style: TextStyle(color: Colors.red),
                        ),
                        onPressed: () => Navigator.pop(context, true),
                      ),
                      TextButton(
                        child: Text("No"),
                        onPressed: () => Navigator.pop(context, false),
                      ),
                    ],
                  ),
                );
          });
        } else if (direction == DismissDirection.endToStart) {
          await Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => NewScreen()),
          );
        }
        return false;
      },
      onDismissed: (_) {},
      background: Container(
        color: Colors.red,
        alignment: Alignment.centerRight,
        padding: EdgeInsets.only(right: 20.0),
        child: Icon(Icons.delete, color: Colors.white),
      ),
      secondaryBackground: Container(
        color: Colors.green,
        alignment: Alignment.centerLeft,
        padding: EdgeInsets.only(left: 20.0),
        child: Icon(Icons.edit, color: Colors.white),
      ),
      child: Padding(
        padding: const EdgeInsets.fromLTRB(8.0, 11.0, 8.0, 15.0),
        child: Card(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10),
          ),
          clipBehavior: Clip.antiAliasWithSaveLayer,
          color: myCustomColor,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: <Widget>[
              Image.asset(
                'lib/images/4.jpg',
                height: 150,
                width: double.infinity,
                fit: BoxFit.cover,
              ),
              Container(
                padding: const EdgeInsets.all(25),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: <Widget>[
                    Text(
                      "Raspored",
                      style: TextStyle(
                        fontSize: 24,
                        color: myCustomColor2,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    Container(height: 10),
                    Text(
                      title,
                      style: const TextStyle(
                        fontSize: 18,
                        color: Colors.white,
                      ),
                    ),
                  ],
                ),
              ),
              Container(height: 5),
            ],
          ),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          "Card List",
          style: TextStyle(
            color: Colors.black54,
          ),
        ),
      ),
      body: ListView(
        children: cards,
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () async {
          final result = await Navigator.push(
            context,
            MaterialPageRoute(builder: (context) => NewScreen()),
          );

          if (result != null) {
            setState(() {
              cards.add(buildCard(result, result));
            });
          }
        },
        child: Icon(Icons.add),
      ),
    );
  }
}

class NewScreen extends StatefulWidget {
  @override
  _NewScreenState createState() => _NewScreenState();
}

class _NewScreenState extends State<NewScreen> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _nameEditingController = TextEditingController();
  final TextEditingController _notesEditingController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset : false,
      appBar: AppBar(
        title: Text('Креирај распоред'),
      ),
      body: Padding(
        padding: const EdgeInsets.fromLTRB(20, 120, 20, 0),
        child: Form(
          key: _formKey,
          child: Column(
            mainAxisAlignment: MainAxisAlignment.start,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              TextFormField(
                controller: _nameEditingController,
                decoration: InputDecoration(
                  labelText: 'Внеси име на распоред *',
                  border: OutlineInputBorder(),
                ),
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return 'Ве молиме внесете име на распоред';
                  }
                  return null;
                },
              ),
              SizedBox(height: 20.0),
              TextFormField(
                controller: _notesEditingController,
                decoration: InputDecoration(
                  labelText: 'Венси забелешки за распоредот',
                  border: OutlineInputBorder(),
                ),
              ),
              SizedBox(height: 30.0),
              Text("Избери тема"),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: <Widget>[
                  ThemeButton(imagePath: 'lib/images/5.jpg', onTap: () {
                    setState(() {
                     // selectedTheme = 'lib/images/5.jpg';
                    });
                  }),
                  ThemeButton(imagePath: 'lib/images/1.jpg', onTap: () {
                    setState(() {
                     // selectedTheme = 'lib/images/1.jpg';
                    });
                  }),
                  ThemeButton(imagePath: 'lib/images/7.jpg', onTap: () {
                    setState(() {
                     // selectedTheme = 'lib/images/7.jpg';
                    });
                  }),
                  ThemeButton(imagePath: 'lib/images/3.jpg', onTap: () {
                    setState(() {
                     // selectedTheme = 'lib/images/3.jpg';
                    });
                  }),
                ],

              ),
              SizedBox(height: 20.0),
              ElevatedButton(
                onPressed: () {
                  if (_formKey.currentState!.validate()) {
                    String name = _nameEditingController.text;
                    String notes = _notesEditingController.text;
                    String result = '$name - $notes';
                    Navigator.pop(context, result);
                  }
                },
                child: Text('Продолжи'),
              ),
            ],
          ),
        ),
      ),
    );
  }
}


class ThemeButton extends StatelessWidget {
  final String imagePath;
  final VoidCallback onTap;

  const ThemeButton({required this.imagePath, required this.onTap});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        margin: EdgeInsets.all(8.0),
        width: 70,
        height: 70,
        decoration: BoxDecoration(
          shape: BoxShape.circle,
          image: DecorationImage(
            image: AssetImage(imagePath),
            fit: BoxFit.cover,
          ),
        ),
      ),
    );
  }
}

