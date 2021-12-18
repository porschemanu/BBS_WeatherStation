from flask import Flask , redirect,url_for, render_template

app = Flask(__name__)

humidity = 50
parameters_name = []
parameters = [1,2,5,3,4]

@app.route("/")
def home():
    return render_template("index.html", test="55")

if __name__ == "__main__":
    app.run()


